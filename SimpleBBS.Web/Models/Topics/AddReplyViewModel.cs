using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBBS.Web.Models.Topics
{
    public class AddReplyViewModel
    {
        public long TopicId { get; set; }
        public long ParentId { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
