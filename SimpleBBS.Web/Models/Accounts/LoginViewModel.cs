using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBBS.Web.Models.Accounts
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "用户名 必填")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "密码 必填")]
        public string Password { get; set; }

    }
}
