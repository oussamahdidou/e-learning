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

        private readonly ITokenService tokenService;


        private readonly IMailer mailer;
        public AccountController(IMailer mailer, ITokenService tokenService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)

        {
            this.tokenService = tokenService;
            this.userManager = userManager;
            this.signInManager = signInManager;

            this.mailer = mailer;

        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.UserName);
            if (user == null)
                return NotFound("invalid username");


            // if (!await userManager.IsEmailConfirmedAsync(user))
            //     return Unauthorized("email is not confirmed");


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
                    return BadRequest(ModelState);

                var user = new AppUser
                {
                    UserName = registerDto.UserName,
                    Email = registerDto.Email,
                };

                var userCreation = await userManager.CreateAsync(user, registerDto.Password);


                if (userCreation.Succeeded)
                {
                    Console.WriteLine("1111111111111111111111111111111111111111111111111111111111111111111111111111.");
                    var userRole = await userManager.AddToRoleAsync(user, "Student");
                    Console.WriteLine("22222222222222222222222222222222222222222222222222222222222222222222222222222.");
                    if (userRole.Succeeded)
                    {
                        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                        Console.WriteLine("3333333333333333333333333333333333333333333333333333333333333333333333333.");
                        var param = new Dictionary<string, string?>
                        {
                            {"token",token},
                            {"email",user.Email}
                        };


                        var callback = QueryHelpers.AddQueryString("http://localhost:4200/auth/verify-email", param);

                        string message = "<!DOCTYPE html><html><head><meta charset='UTF-8'><meta name='viewport' content='width=device-width, initial-scale=1.0'><title>Confirm Your Email Address</title><style>body {font-family: sans-serif;margin: 0;padding: 0;}.container {padding: 20px;max-width: 600px;margin: 0 auto;border: 1px solid #ddd;border-radius: 5px;}header {text-align: center;margin-bottom: 20px;}h1 {font-size: 24px;}p {line-height: 1.5;}a.confirm-button {display: block;padding: 10px 20px;background-color: #4CAF50;color: white;text-decoration: none;border: none;border-radius: 5px;text-align: center;}.confirm-button:hover {background-color: #3e8e41;}</style></head><body><div class='container'><header><h1>Welcome to E-Learning Plateform</h1></header><p>Hi " + user.UserName + " ,</p><p>Welcome to in E-Learning plateform we sent this email for your account activation . To complete your email confirmation, please click the button below:</p><center><a href=" + callback + " class='confirm-button'>Confirm Email</a></center><p>If you can't click the button, please copy and paste the following link into your web browser:</p><p>" + callback + "</p><p>**Please note:** This link will expire in 2 hours.</p><p>Thanks,</p><p>The E-Learning Team</p></div></body></html>";


                        await mailer.SendEmailAsync(user.Email, "Email Confirmation Token", message);
                        Console.WriteLine("44444444444444444444444444444444444444444444444444444444444444444444444444.");

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
                        return StatusCode(500, userRole.Errors);
                    }

                }
                else
                {
                    return StatusCode(500, userCreation.Errors);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("777777777777777777777777777777777777777 : " + e);
                return StatusCode(500, e);
            }
        }
        [HttpGet("emailconfirmation")]
        public async Task<IActionResult> EmailConfirmation([FromQuery] string email, [FromQuery] string token)
        {
            var user = await userManager.FindByEmailAsync(email);
            Console.WriteLine("11111111111111111111111111111111111111111111111111111111111111111111111111.");
            if (user is null)
            {
                Console.WriteLine(email);
                Console.WriteLine("15151515151515151515151515151515151515151515151515.");
                return BadRequest("Invalid Email Confirmation Request");
            }

            var confirm = await userManager.ConfirmEmailAsync(user, token);
            Console.WriteLine("2222222222222222222222222222222222222222222222222222222222222222222222222.");
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
                Console.WriteLine("1111111111111111111111111111111111111111111111111111111111111111111111111111111.");
                return BadRequest();
            }
            var user = await userManager.FindByEmailAsync(forgotPasswordDto.Email!);
            Console.WriteLine("2222222222222222222222222222222222222222222222222222222222222222222222222.");
            if (user is null)
            {
                Console.WriteLine("3333333333333333333333333333333333333333333333333333333333333333333333333333333.");
                return BadRequest("Invalid Request");
            }

            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var param = new Dictionary<string, string?>
            {
                {"token",token},
                {"email",forgotPasswordDto.Email}
            };
            var callback = QueryHelpers.AddQueryString("http://localhost:4200/auth/reset-password", param);
            string message = "<!DOCTYPE html><html><head><meta charset='UTF-8'><meta name='viewport' content='width=device-width, initial-scale=1.0'><title>Confirm Your Email Address</title><style>body {font-family: sans-serif;margin: 0;padding: 0;}.container {padding: 20px;max-width: 600px;margin: 0 auto;border: 1px solid #ddd;border-radius: 5px;}header {text-align: center;margin-bottom: 20px;}h1 {font-size: 24px;}p {line-height: 1.5;}a.confirm-button {display: block;padding: 10px 20px;background-color: #4CAF50;color: white;text-decoration: none;border: none;border-radius: 5px;text-align: center;}.confirm-button:hover {background-color: #3e8e41;}</style></head><body><div class='container'><header><h1>E-Learning</h1></header><p>Hi " + user.UserName + " ,</p><p>You sent a password reset request in E-Learning plateform. To complete your password reset, please click the button below:</p><center><a href=" + callback + " class='confirm-button'>Reset Password</a></center><p>If you can't click the button, please copy and paste the following link into your web browser:</p><p>" + callback + "</p><p>**Please note:** This link will expire in 2 hours.</p><p>Thanks,</p><p>The E-Learning Team</p></div></body></html>";
            await mailer.SendEmailAsync(user.Email, "Reset Password Token", message);
            Console.WriteLine("4444444444444444444444444444444444444444.");
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


    }
}