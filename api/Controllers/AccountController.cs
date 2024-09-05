using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Account;
using api.interfaces;
using api.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly IBlobStorageService _blobStorageService;
        private readonly ITokenService tokenService;


        private readonly IMailer mailer;
        public AccountController(IMailer mailer, ITokenService tokenService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IBlobStorageService blobStorageService)

        {
            this.tokenService = tokenService;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this._blobStorageService = blobStorageService;
            this.mailer = mailer;

        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {

            var user = await userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.UserName);
            if (user == null)
                return NotFound("invalid username");


            if (!await userManager.IsEmailConfirmedAsync(user))
                return Unauthorized("email is not confirmed");


            var userconnected = await signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!userconnected.Succeeded)
                return NotFound("invalid password");

            return Ok(new NewUserDto()
            {

                //Username = user.UserName,
                //Email = user.Email,
                Username = user.UserName ?? string.Empty,
                Email = user.Email ?? string.Empty,
                Token = await tokenService.CreateToken(user)
            });
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Incorrect Fields Format, Try Again");

                var user = new Student
                {
                    Nom = registerDto.Nom,
                    Prenom = registerDto.Prenom,
                    DateDeNaissance = (DateTime)registerDto.DateDeNaissance,
                    Etablissement = registerDto.Etablissement,
                    Branche = registerDto.Branche,
                    Niveaus = registerDto.Niveaus,
                    UserName = registerDto.UserName,
                    Email = registerDto.Email,
                    TuteurMail = registerDto.TuteurMail,
                    PhoneNumber = registerDto.PhoneNumber,
                };

                var userCreation = await userManager.CreateAsync(user, registerDto.Password);


                if (userCreation.Succeeded)
                {
                    var userRole = await userManager.AddToRoleAsync(user, "Student");
                    if (userRole.Succeeded)
                    {
                        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                        var param = new Dictionary<string, string?>
                        {
                            {"token",token},
                            {"email",user.Email}
                        };


                        // var callback = QueryHelpers.AddQueryString("http://localhost:4200/auth/verify-email", param);
                        var callback = QueryHelpers.AddQueryString("https://elearningwebclient.azurewebsites.net/auth/verify-email", param);

                        string message = "<!DOCTYPE html><html><head><meta charset='UTF-8'><meta name='viewport' content='width=device-width, initial-scale=1.0'><title>Confirm Your Email Address</title><style>body {font-family: sans-serif;margin: 0;padding: 0;}.container {padding: 20px;max-width: 600px;margin: 0 auto;border: 1px solid #ddd;border-radius: 5px;}header {text-align: center;margin-bottom: 20px;}h1 {font-size: 24px;}p {line-height: 1.5;}a.confirm-button {display: block;padding: 10px 20px;background-color: #4CAF50;color: white;text-decoration: none;border: none;border-radius: 5px;text-align: center;}.confirm-button:hover {background-color: #3e8e41;}</style></head><body><div class='container'><header><h1>Welcome to E-Learning Plateform</h1></header><p>Hi " + user.UserName + " ,</p><p>Welcome to in E-Learning plateform we sent this email for your account activation . To complete your email confirmation, please click the button below:</p><center><a href=" + callback + " class='confirm-button'>Confirm Email</a></center><p>If you can't click the button, please copy and paste the following link into your web browser:</p><p>" + callback + "</p><p>**Please note:** This link will expire in 2 hours.</p><p>Thanks,</p><p>The E-Learning Team</p></div></body></html>";


                        await mailer.SendEmailAsync(user.Email, "Email Confirmation Token", message);

                        return Ok(
                            new NewUserDto()
                            {
                                Username = user.UserName,
                                Email = user.Email,
                                Token = "please verify your email"
                            }
                        );
                    }
                    else
                    {
                        var errors = string.Join(", ", userRole.Errors.Select(e => e.Description));
                        Console.WriteLine("User role failed: " + errors);
                        return BadRequest("User role failed: " + errors);
                        // return StatusCode(500, userRole.Errors);
                    }

                }
                else
                {
                    var errors = string.Join(", ", userCreation.Errors.Select(e => e.Description));
                    Console.WriteLine("User creation failed: " + errors);
                    return BadRequest("User creation failed: " + errors);
                    // return StatusCode(500, userCreation.Errors);
                }

            }
            catch (Exception e)
            {
                return BadRequest("An Error Occured");
                // return StatusCode(500, e);
            }
        }

        [HttpPost("TeacherRegister")]
        public async Task<IActionResult> TeacherRegister([FromForm] TeacherRegisterDto teacherRegisterDto)
        {
            try
            {
                var justification = "";

                if (!ModelState.IsValid)
                    return BadRequest("Incorrect Fields Format, Try Again");

                if (teacherRegisterDto.JustificatifDeLaProfession != null)
                {

                    justification = await _blobStorageService.UploadFileAsync(teacherRegisterDto.JustificatifDeLaProfession.OpenReadStream(), "schema-container", teacherRegisterDto.JustificatifDeLaProfession.FileName);
                    Console.WriteLine($"le fichier justification {justification}");
                }

                Console.WriteLine("222222222222222222222222222222222222222222222222");

                var user = new Teacher
                {
                    Nom = teacherRegisterDto.Teacher_Nom,
                    Prenom = teacherRegisterDto.Teacher_Prenom,
                    DateDeNaissance = (DateTime)(teacherRegisterDto.Teacher_DateDeNaissance),
                    Etablissement = teacherRegisterDto.Teacher_Etablissement,
                    JustificatifDeLaProfession = justification,
                    UserName = teacherRegisterDto.UserName,
                    Email = teacherRegisterDto.Email,
                    Granted = false,
                    Status = teacherRegisterDto.Status,
                    Specialite = teacherRegisterDto.Specialite,
                    PhoneNumber = teacherRegisterDto.PhoneNumber,
                };

                var userCreation = await userManager.CreateAsync(user, teacherRegisterDto.Password);
                Console.WriteLine("api ::::: 11111111111111111111111111111111111111111");


                if (userCreation.Succeeded)
                {
                    Console.WriteLine(" api ::::: 222222222222222222222222222222222222222222");
                    var userRole = await userManager.AddToRoleAsync(user, "Teacher");
                    if (userRole.Succeeded)
                    {
                        Console.WriteLine(" api ::::: 3333333333333333333333333333333333333333333");
                        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                        var param = new Dictionary<string, string?>
                        {
                            {"token",token},
                            {"email",user.Email}
                        };


                        // var callback = QueryHelpers.AddQueryString("http://localhost:4200/auth/verify-email", param);
                        var callback = QueryHelpers.AddQueryString("https://elearningwebclient.azurewebsites.net/auth/verify-email", param);

                        string message = "<!DOCTYPE html><html><head><meta charset='UTF-8'><meta name='viewport' content='width=device-width, initial-scale=1.0'><title>Confirm Your Email Address</title><style>body {font-family: sans-serif;margin: 0;padding: 0;}.container {padding: 20px;max-width: 600px;margin: 0 auto;border: 1px solid #ddd;border-radius: 5px;}header {text-align: center;margin-bottom: 20px;}h1 {font-size: 24px;}p {line-height: 1.5;}a.confirm-button {display: block;padding: 10px 20px;background-color: #4CAF50;color: white;text-decoration: none;border: none;border-radius: 5px;text-align: center;}.confirm-button:hover {background-color: #3e8e41;}</style></head><body><div class='container'><header><h1>Welcome to E-Learning Plateform</h1></header><p>Hi " + user.UserName + " ,</p><p>Welcome to in E-Learning plateform we sent this email for your account activation . To complete your email confirmation, please click the button below:</p><center><a href=" + callback + " class='confirm-button'>Confirm Email</a></center><p>If you can't click the button, please copy and paste the following link into your web browser:</p><p>" + callback + "</p><p>**Please note:** This link will expire in 2 hours.</p><p>Thanks,</p><p>The E-Learning Team</p></div></body></html>";


                        await mailer.SendEmailAsync(user.Email, "Email Confirmation Token", message);

                        return Ok(
                            new NewUserDto()
                            {
                                Username = user.UserName,
                                Email = user.Email,
                                Token = "please verify your email"
                            }
                        );
                    }
                    else
                    {
                        var errors = string.Join(", ", userRole.Errors.Select(e => e.Description));
                        Console.WriteLine("User role failed: " + errors);
                        return BadRequest("User role failed: " + errors);
                        // return StatusCode(500, userRole.Errors);
                    }

                }
                else
                {
                    var errors = string.Join(", ", userCreation.Errors.Select(e => e.Description));
                    Console.WriteLine("User creation failed: " + errors);
                    return BadRequest("User creation failed: " + errors);
                    // return StatusCode(500, userCreation.Errors);
                }

            }
            catch (Exception e)
            {
                return BadRequest("An Error Occured");
                // return StatusCode(500, e);
            }
        }


        [HttpGet("emailconfirmation")]
        public async Task<IActionResult> EmailConfirmation([FromQuery] string email, [FromQuery] string token)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user is null)
            {
                Console.WriteLine(email);
                return BadRequest("Invalid Email Confirmation Request");
            }

            var confirm = await userManager.ConfirmEmailAsync(user, token);
            if (!confirm.Succeeded)
                return BadRequest("Invalid Email Confirmation Request");


            return Ok(new NewUserDto()
            {
                Username = user.UserName,
                Email = user.Email,
                Token = "Your Email is confirmed successfuly"
            });
        }
        [HttpPost("forgotpassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var user = await userManager.FindByEmailAsync(forgotPasswordDto.Email!);
            if (user is null)
            {
                return BadRequest("Invalid Request");
            }

            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var param = new Dictionary<string, string?>
            {
                {"token",token},
                {"email",forgotPasswordDto.Email}
            };
            // var callback = QueryHelpers.AddQueryString("http://localhost:4200/auth/reset-password", param);
            var callback = QueryHelpers.AddQueryString("https://elearningwebclient.azurewebsites.net/auth/reset-password", param);
            string message = "<!DOCTYPE html><html><head><meta charset='UTF-8'><meta name='viewport' content='width=device-width, initial-scale=1.0'><title>Confirm Your Email Address</title><style>body {font-family: sans-serif;margin: 0;padding: 0;}.container {padding: 20px;max-width: 600px;margin: 0 auto;border: 1px solid #ddd;border-radius: 5px;}header {text-align: center;margin-bottom: 20px;}h1 {font-size: 24px;}p {line-height: 1.5;}a.confirm-button {display: block;padding: 10px 20px;background-color: #4CAF50;color: white;text-decoration: none;border: none;border-radius: 5px;text-align: center;}.confirm-button:hover {background-color: #3e8e41;}</style></head><body><div class='container'><header><h1>E-Learning</h1></header><p>Hi " + user.UserName + " ,</p><p>You sent a password reset request in E-Learning plateform. To complete your password reset, please click the button below:</p><center><a href=" + callback + " class='confirm-button'>Reset Password</a></center><p>If you can't click the button, please copy and paste the following link into your web browser:</p><p>" + callback + "</p><p>**Please note:** This link will expire in 2 hours.</p><p>Thanks,</p><p>The E-Learning Team</p></div></body></html>";
            await mailer.SendEmailAsync(user.Email, "Reset Password Token", message);
            return Ok(
                    new NewUserDto()
                    {
                        Username = user.UserName,
                        Email = user.Email,
                        Token = "Reset Password link Sent To Your EMail"
                    }
                    );
        }
        [HttpPost("resetpassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var user = await userManager.FindByEmailAsync(resetPasswordDto.Email!);
            if (user is null)
                return BadRequest("Invalid Request");

            var result = await userManager.ResetPasswordAsync(user, resetPasswordDto.Token!, resetPasswordDto.Password!);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);

                return BadRequest(new { Errors = errors });
            }
            return Ok(new NewUserDto()
            {
                Username = user.UserName,
                Email = user.Email,
                Token = "Your Password reset successfuly"
            }
                );
        }
        [HttpDelete("deleteuser/{Id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] string Id)
        {
            Console.WriteLine("00000000000000000000000000000000000000000");
            var user = await userManager.FindByIdAsync(Id);
            Console.WriteLine("11111111111111111111111111111111111111111111111");
            if (user is null)
            {
                return BadRequest("User Not Found");
            }
            else
            {
                var result = await userManager.DeleteAsync(user);
                Console.WriteLine("222222222222222222222222222222222222222222222222");
                if (result.Succeeded)
                {
                    return Ok(new AuthNotificationDto()
                    {
                        Message = "User Deleted Successfuly"
                    }
                    );
                }
                else
                {
                    return BadRequest("User Not Deleted");
                }
            }
        }
    }
}