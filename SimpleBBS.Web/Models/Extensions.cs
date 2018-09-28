using SimpleBBS.Core;
using SimpleBBS.Web.Models.Topics;
using SimpleBBS.Web.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBBS.Web.Models
{
    public static class Extensions
    {
        public static TopicListItemViewModel ToTopicListItemModel(this Topic entity)
        {
            return new TopicListItemViewModel()
            {
                Id = entity.Id,
                //Guid = entity.Guid,
                LastModificationTime = entity.LastModificationTime,
                PublishedTime = entity.PublishedTime,
                ReplyedCount = entity.ReplyedCount,
                Status = entity.Status,
                Tags = entity.Tags,
                Title = entity.Title,
                TagsId = entity.TagsId,
                UserId = entity.UserId,
                ViewedCount = entity.ViewedCount,
                User = entity.User?.ToModel()
            };
        }

        public static TopicDetailsViewModel ToTopicDetailsModel(this Topic entity)
        {
            return new TopicDetailsViewModel()
            {
                Id = entity.Id,
                Content = entity.Content,
                //Guid = entity.Guid,
                LastModificationTime = entity.LastModificationTime,
                PublishedTime = entity.PublishedTime,
                ReplyedCount = entity.ReplyedCount,
                Status = entity.Status,
                Tags = entity.Tags,
                Title = entity.Title,
                TagsId = entity.TagsId,
                UserId = entity.UserId,
                ViewedCount = entity.ViewedCount,
                User = entity.User?.ToModel()
            };
        }

        public static ReplyViewModel ToModel(this Reply entity)
        {
            return new ReplyViewModel()
            {
                Content = entity.Content,
                Forbided = entity.Forbided,
                CreationTime = entity.CreationTime,
                Id = entity.Id,
                ParentId = entity.ParentId,
                UpCount = entity.UpCount,
                UserId = entity.UserId,
                User = entity.User?.ToModel(),
            };
        }

        public static UserBaseViewModel ToModel(this User entity)
        {
            return new UserBaseViewModel()
            {
                Email = entity.Email,
                Id = entity.Id,
                UserName = entity.UserName,
            };
        }
    }
}
