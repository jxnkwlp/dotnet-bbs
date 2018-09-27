﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleBBS.Core
{
    public class User : IdentityUser<long>, IEntity
    {

        public DateTime? LastLoginedTime { get; set; }




        public User()
        {

        }



    }
}
