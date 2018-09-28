using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleBBS.Core
{
    public class UserInfo
    {
        public long UserId { get; set; }
        public User User { get; set; }

        public string SiteUrl { get; set; }
        public string City { get; set; }
        public string WeiboId { get; set; }
        public string GitHubId { get; set; }
        public string UserSign { get; set; }
        public string Description { get; set; }
    }
}
