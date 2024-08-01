using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.UserCenter
{
    public class ChangePasswordDto
    {
        public string CurrentPassword { get; set; } = "";
        public string NewPassword { get; set; } = "";
        public string RepeatPassword { get; set; } = "";
    }
}