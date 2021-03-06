﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleBBS.Core
{
    public class Topic : BaseEntity
    {
        // public Guid Guid { get; set; } = Guid.NewGuid();

        public string Title { get; set; }

        public string Content { get; set; }

        public long UserId { get; set; }
        public User User { get; set; }

        public long ViewedCount { get; set; }

        public long ReplyedCount { get; set; }

        public DateTime? LastModificationTime { get; set; }

        public DateTime PublishedTime { get; set; }

        public bool Deleted { get; set; } = false;

        public TopicStatus Status { get; set; }

        public long TagsId { get; set; }

        public Tags Tags { get; set; }

    }
}
