using Microsoft.AspNetCore.Mvc;
using SimpleBBS.Web.Models;
using SimpleBBS.Web.Models.Topics;
using SimpleBBS.Web.Services;

namespace SimpleBBS.Web.Components
{
    public class UserJoinedTopicViewComponent : ViewComponent
    {
        private readonly TopicService _topicService;
        private readonly ReplyService _replyService;

        public UserJoinedTopicViewComponent(TopicService topicService, ReplyService replyService)
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

            var list = _topicService.GetListByUserLastReply(1, count, userId);

            TopicListViewModel model = new TopicListViewModel();

            model.List = list.ReplaceTo((s) =>
            {
                return s.ToTopicListItemModel();
            });

            return View(model);
        }
    }
}
