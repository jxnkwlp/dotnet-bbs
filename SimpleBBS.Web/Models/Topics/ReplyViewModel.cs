using SimpleBBS.Core;
using SimpleBBS.Web.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBBS.Web.Models.Topics
{
    public class ReplyViewModel
    {
        public long Id { get; set; }

        public long ParentId { get; set; }

        public string Content { get; set; }

        public long UserId { get; set; }

        public UserBaseViewModel User { get; set; }



        public long ParentUserId { get; set; }

        public UserBaseViewModel ParentUser { get; set; }


        public long UpCount { get; set; }

        public bool Forbided { get; set; }

        public DateTime CreationTime { get; set; }
    }
}
