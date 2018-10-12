using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleBBS.Core
{
    public class User : IdentityUser<long>, IEntity, ICreationTimeEntity
    {

        public DateTime? LastLoginedTime { get; set; }

        public DateTime CreationTime { get; set; }

        public UserInfo UserInfo { get; set; }


        public User()
        {
        }



    }
}
