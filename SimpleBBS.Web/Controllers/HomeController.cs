using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleBBS.Models;
using SimpleBBS.Web.Models;
using SimpleBBS.Web.Models.Topics;
using SimpleBBS.Web.Services;

namespace SimpleBBS.Web.Controllers
{
    public class HomeController : WebBaseController
    {
        private readonly TopicService _topicService;
        private readonly ReplyService _replyService;

        public HomeController(TopicService topicService, ReplyService replyService)
        {
            _topicService = topicService;
            _replyService = replyService;

        }


        public IActionResult Index(int page = 1)
        {
            if (page <= 1) page = 1;

            var list = _topicService.GetLastedList(page, 15);

            var model = new TopicListViewModel();

            model.List = list.ReplaceTo(t => t.ToTopicListItemModel());

            var lastReplyList = _replyService.GetLastByTopicIds(list.Select(t => t.Id).ToArray());

            foreach (var item in model.List)
            {
                var reply = lastReplyList.FirstOrDefault(t => t.TopicId == item.Id);
                if (reply != null)
                    item.LastReply = reply.ToModel();
            }

            return View(model);
        }


        public IActionResult Test()
        {
            _topicService.UpdateGuid();


            return Content("OK");
        }



        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
