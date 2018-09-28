using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleBBS.Core
{
    public class Reply : BaseEntity
    {
        public long TopicId { get; set; }

        public Topic Topic { get; set; }

        public long ParentId { get; set; }

        public string Content { get; set; }

        public long UserId { get; set; }
        public User User { get; set; }

        public long UpCount { get; set; }

        public bool Forbided { get; set; }

    }
}
