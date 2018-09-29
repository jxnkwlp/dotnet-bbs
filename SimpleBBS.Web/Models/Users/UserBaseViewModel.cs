using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBBS.Web.Models.Users
{
    public class UserBaseViewModel
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public string UserSign { get; set; }
    }
}
