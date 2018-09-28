using SimpleBBS.Core;
using SimpleBBS.Web.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBBS.Web.Models.Topics
{
    public class TopicDetailsViewModel
    {
        public long Id { get; set; }
        public Guid Guid { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public long UserId { get; set; }
        public UserBaseViewModel User { get; set; }

        public long ViewedCount { get; set; }

        public long ReplyedCount { get; set; }

        public DateTime? LastModificationTime { get; set; }

        public DateTime PublishedTime { get; set; }

        public TopicStatus Status { get; set; }

        public long TagsId { get; set; }

        public Tags Tags { get; set; }


        public IList<ReplyViewModel> Replies { get; set; }
    }
}
