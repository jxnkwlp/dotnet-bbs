﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBBS.Web.Models.Accounts
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Email 必填")]
        [MaxLength(32)]
        [EmailAddress(ErrorMessage = "Email 格式不正确")]
        public string Email { get; set; }
         
        [Required(ErrorMessage = "密码 必填")]
        [MaxLength(16, ErrorMessage = "密码长度不超过16位")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "两次密码输入不一致")]
        public string ConfirmPassword { get; set; }
    }
}
