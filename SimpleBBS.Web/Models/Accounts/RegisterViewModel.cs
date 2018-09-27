using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBBS.Web.Models.Accounts
{
    public class RegisterViewModel
    {
        [Required]
        [MaxLength(32)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(16)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
