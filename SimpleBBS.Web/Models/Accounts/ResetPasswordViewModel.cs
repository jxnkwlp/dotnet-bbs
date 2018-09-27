using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBBS.Web.Models.Accounts
{
    public class ResetPasswordViewModel
    {
        public long UserId { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [MaxLength(16, ErrorMessage = "密码长度不超过16位")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "两次密码输入不一致")]
        public string ConfirmPassword { get; set; }
    }
}
