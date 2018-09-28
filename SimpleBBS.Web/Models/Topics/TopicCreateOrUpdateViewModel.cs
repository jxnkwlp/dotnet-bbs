using SimpleBBS.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBBS.Web.Models.Topics
{
    public class TopicCreateOrUpdateViewModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "标题 必填")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "标题长度介于5-100个字符")]
        public string Title { get; set; }

        // [AllowHtml]
        [Required(ErrorMessage = "内容 必填")]
        public string Content { get; set; }

        public long TagsId { get; set; }

        public IList<Tags> AllTags { get; set; }

    }
}
