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
        public AccountController(IMailer mailer,ITokenService tokenService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            this.tokenService = tokenService;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.mailer=mailer;
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
                Username = user.UserName,
                Email = user.Email,
                Token = await tokenService.CreateToken(user)
            });
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try{
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);
                
                var user = new AppUser {
                    UserName = registerDto.UserName,
                    Email= registerDto.Email,
                };

                var userCreation = await userManager.CreateAsync(user,registerDto.Password);

                if(userCreation.Succeeded)
                {
                    var userRole = await userManager.AddToRoleAsync(user,"Student");
                    if(userRole.Succeeded)
                    {
                        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                        var param = new Dictionary<string,string?>
                        {
                            {"token",token},
                            {"email",user.Email}
                        };

                        var callback = QueryHelpers.AddQueryString(registerDto.confirmationUri!,param);

                        await mailer.SendEmailAsync(user.Email,"Email Confirmation Token",callback);

                        return Ok(
                            new NewUserDto()
                            {
                                Username = user.UserName,
                                Email = user.Email,
                                Token = await tokenService.CreateToken(user)
                            }
                        );
                    }else{
                        return StatusCode(500,userRole.Errors);
                    }
                    
                }else
                {
                    return StatusCode(500,userCreation.Errors);
                }

            }catch(Exception e){
                return StatusCode(500,e);
            }
        }
        [HttpGet("emailconfirmation")]
        public async Task<IActionResult> EmailConfirmation([FromQuery] string email,[FromQuery] string token)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user is null)
                return BadRequest("Invalid Email Confirmation Request");
            
            var confirm = await userManager.ConfirmEmailAsync(user,token);
            if(!confirm.Succeeded)
                return BadRequest("Invalid Email Confirmation Request");


            return Ok("Email verified");
        }
    }
}