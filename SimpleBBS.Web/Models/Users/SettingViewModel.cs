using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBBS.Web.Models.Users
{
    public class SettingViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string SiteUrl { get; set; }
        public string City { get; set; }
        public string WeiboId { get; set; }
        public string GitHubId { get; set; }
        public string UserSign { get; set; }

    }
}
