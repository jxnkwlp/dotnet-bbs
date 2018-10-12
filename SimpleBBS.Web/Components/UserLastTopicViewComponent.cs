using Microsoft.AspNetCore.Mvc;
using SimpleBBS.Web.Models;
using SimpleBBS.Web.Models.Topics;
using SimpleBBS.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBBS.Web.Components
{
    public class UserLastTopicViewComponent : ViewComponent
    {
        private readonly TopicService _topicService;
        private readonly ReplyService _replyService;

        public UserLastTopicViewComponent(TopicService topicService, ReplyService replyService)
        {
            _topicService = topicService;
            _replyService = replyService;

        }

        public IViewComponentResult Invoke()
        {
            int count = 10;

            if (!User.Identity.IsAuthenticated)
            {
                return Content("");
            }

            var userId = User.Identity.GetUserId<int>();

            var list = _topicService.GetLastedList(1, count, userId: userId);

            TopicListViewModel model = new TopicListViewModel();

            model.List = list.ReplaceTo((s) =>
            {
                return s.ToTopicListItemModel();
            });

            return View(model);
        }

    }
}
