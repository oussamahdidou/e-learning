using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.UserCenter;
using api.generique;
using api.Model;

namespace api.interfaces
{
    public interface IUserCenterInterface
    {
        Task<Result<bool>> ChangePassword(AppUser user , ChangePasswordDto changePasswordDto);
    }
}