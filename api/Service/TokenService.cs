using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using api.interfaces;
using api.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;

namespace api.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration configuration;
        private readonly SymmetricSecurityKey symmetricSecurityKey;
        private readonly UserManager<AppUser> userManager;
        public TokenService(IConfiguration configuration, UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SigninKey"]));
        }
        public async Task<string> CreateToken(AppUser appUser)
        {
            var roles = await userManager.GetRolesAsync(appUser);
            List<Claim> claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email,appUser.Email ),
                new Claim(JwtRegisteredClaimNames.GivenName,appUser.UserName),
                new Claim(JwtRegisteredClaimNames.UniqueName,appUser.Id)
            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = credentials,
                Issuer = configuration["JWT:Issuer"],
                Audience = configuration["JWT:Audience"]
            };

            var tokenhandler = new JwtSecurityTokenHandler();
            var token = tokenhandler.CreateToken(tokenDescriptor);


            return tokenhandler.WriteToken(token);
        }
    }
}