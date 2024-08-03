using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.UserCenter;
using api.generique;
using api.interfaces;
using api.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata;

namespace api.Repository
{
    public class UserCenterRepository : IUserCenterInterface
    {
        private readonly UserManager<AppUser> _userManager;
        public UserCenterRepository(UserManager<AppUser> userManager)

        {
            _userManager = userManager;
        }
        public async Task<Result<bool>> ChangePassword(AppUser user, ChangePasswordDto changePasswordDto)
        {
            var result = await _userManager.ChangePasswordAsync(user, changePasswordDto.CurrentPassword, changePasswordDto.NewPassword);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    // Use your logging framework here
                    Console.WriteLine($"Error: {error.Code} - {error.Description}");
                }
                return Result<bool>.Failure("Something went wrong");
            }
            return Result<bool>.Success(true);
        }
    }
}