using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleBBS.Core
{
    public class Topic : BaseEntity
    {
        public Guid Guid { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public int UserId { get; set; }

        public long ViewedCount { get; set; }

        public DateTime? LastModificationTime { get; set; }

        public DateTime PublishedTime { get; set; }

        public TopicStatus Status { get; set; }



    }
}
