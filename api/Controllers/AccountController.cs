using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Account;
using api.interfaces;
using api.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        public AccountController(ITokenService tokenService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            this.tokenService = tokenService;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.UserName);
            if (user == null)
                return NotFound("invalid username");
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
    }
}