using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBBS.Web.Models.Accounts
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Email 必填")]
        [MaxLength(32)]
        [EmailAddress(ErrorMessage = "Email 格式不正确")]
        public string Email { get; set; }


        public bool Result { get; set; }

    }
}
