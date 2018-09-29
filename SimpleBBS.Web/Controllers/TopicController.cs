using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleBBS.Core;
using SimpleBBS.Web.Models;
using SimpleBBS.Web.Models.Topics;
using SimpleBBS.Web.Services;

namespace SimpleBBS.Web.Controllers
{
    [Authorize]
    public class TopicController : WebBaseController
    {
        private readonly TopicService _topicService;
        private readonly TagsService _tagsService;
        private readonly ReplyService _replyService;

        public TopicController(TopicService topicService, TagsService tagsService, ReplyService replyService)
        {
            _topicService = topicService;
            _tagsService = tagsService;
            _replyService = replyService;

        }

        private void PrepareViewModel(TopicCreateOrUpdateViewModel viewModel)
        {
            viewModel.AllTags = _tagsService.GetList();
        }

        private async Task<TopicDetailsViewModel> PrepareViewModelAsync(TopicDetailsViewModel viewModel, Topic topic)
        {
            viewModel = topic.ToTopicDetailsModel();

            viewModel.Replies = await LoadReplyListAsync(topic.Id);

            return viewModel;
        }


        [AllowAnonymous]
        public async Task<IActionResult> Details(long id)
        {
            var userId = User.Identity.GetUserId<long>();

            var topic = await _topicService.GetTopicByIdAsync(id);

            if (topic == null)
                return NotFound();

            if (topic.Deleted)
                return NotFound();

            if (topic.Status != TopicStatus.Published)
                return NotFound();


            var model = new TopicDetailsViewModel();

            model = await PrepareViewModelAsync(model, topic);

            await _topicService.IncreaseViewCountAsync(topic);



            model.ShowEditBar = topic.UserId == userId;

            return View(model);
        }


        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            var userId = User.Identity.GetUserId<long>();

            var topic = await _topicService.GetTopicByIdAsync(id, false, false);

            if (topic != null)
            {
                if (topic.UserId != userId)
                {
                    return Json(new { result = false });
                }

                try
                {
                    await _topicService.MakeAsDeletedAsync(id);

                    return Json(new { result = true });
                }
                catch (Exception)
                {
                }

            }

            return Json(new { result = false });
        }


        #region Create

        public IActionResult Create()
        {
            var model = new TopicCreateOrUpdateViewModel();

            PrepareViewModel(model);

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TopicCreateOrUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId<long>();

                var entity = new Topic()
                {
                    Title = model.Title,
                    Content = model.Content,
                    // Guid = Guid.NewGuid(),
                    Status = TopicStatus.Published,  // default published 
                    PublishedTime = DateTime.Now,
                    TagsId = model.TagsId,
                    UserId = userId,
                };

                try
                {
                    await _topicService.AddAsync(entity);

                    return RedirectToAction(nameof(Details), new { id = entity.Id });
                }
                catch (Exception)
                {

                }

            }

            PrepareViewModel(model);
            return View(model);
        }

        #endregion

        #region Edit

        public async Task<IActionResult> Edit(int id)
        {
            var userId = User.Identity.GetUserId<long>();

            var topic = await _topicService.GetTopicByIdAsync(id, false, false);

            if (topic == null)
                return NotFound();

            if (topic.UserId != userId)
                return NotFound();

            var model = topic.ToEditModel();

            PrepareViewModel(model);

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TopicCreateOrUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId<long>();

                var entity = await _topicService.GetTopicByIdAsync(model.Id, false, false);
                if (entity == null)
                    return NotFound();

                entity.Content = model.Content;
                entity.TagsId = model.TagsId;
                entity.Title = model.Title;

                entity.PublishedTime = DateTime.Now;
                entity.LastModificationTime = DateTime.Now;

                try
                {
                    await _topicService.UpdateAsync(entity);

                    return RedirectToAction(nameof(Details), new { id = entity.Id });
                }
                catch (Exception)
                { 
                }

            }

            PrepareViewModel(model);
            return View(model);
        }

        #endregion

        #region reply

        private async Task<IList<ReplyViewModel>> LoadReplyListAsync(long topicId)
        {
            var list = await _replyService.GetListByTopicIdAsync(topicId);

            var result = list.Select(t => t.ToModel()).ToList();

            foreach (var item in result)
            {
                if (item.ParentId > 0)
                {
                    var parent = result.FirstOrDefault(t => t.Id == item.ParentId);
                    if (parent != null)
                    {
                        item.ParentUserId = parent.UserId;
                        item.ParentUser = parent.User;
                    }
                }
            }

            return result;
        }


        [HttpPost]
        public async Task<ActionResult> SubmitReply(AddReplyViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId<long>();

                if (model.TopicId <= 0) return Json(new { result = false });

                try
                {
                    await _replyService.AddAsync(new Reply()
                    {
                        Content = model.Content,
                        ParentId = model.ParentId,
                        UserId = userId,
                        TopicId = model.TopicId,
                    });

                    await _topicService.IncreaseReplyCountAsync(model.TopicId);

                    return Json(new { result = true });
                }
                catch
                {
                }

            }
            return Json(new { result = false });
        }



        #endregion

        #region ReplyUp



        #endregion

    }
}