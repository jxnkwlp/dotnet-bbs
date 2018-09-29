using Microsoft.EntityFrameworkCore;
using SimpleBBS.Core;
using SimpleBBS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBBS.Web.Services
{
    public class TopicService
    {
        private readonly ApplicationDbContext _dbContext;

        public TopicService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

        }

        public async Task AddAsync(Topic entity)
        {
            _dbContext.Add(entity);

            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Topic entity)
        {
            _dbContext.Update(entity);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<Topic> GetTopicByIdAsync(long id, bool loadUser = true, bool loadTags = true)
        {
            var entity = await _dbContext.Topics.FindAsync(id);

            if (entity != null)
            {
                if (loadUser)
                {
                    await _dbContext.Entry(entity).Reference(t => t.User).LoadAsync();
                }
                if (loadTags)
                {
                    await _dbContext.Entry(entity).Reference(t => t.Tags).LoadAsync();
                }
            }

            return entity;
        }

        internal void UpdateGuid()
        {
            //  _dbContext.Database.ExecuteSqlCommand("update Topics set Guid=@v ", new { v = Guid.NewGuid().ToString() });
        }

        public IPagedList<Topic> GetLastedList(int page, int pageSize, long? tagsId = null)
        {
            var query = _dbContext.Topics
                .Where(t => !t.Deleted && t.Status == TopicStatus.Published)
                .OrderByDescending(t => t.CreationTime)
                .AsQueryable();

            if (tagsId.HasValue)
            {
                query = query.Where(t => t.TagsId == tagsId.Value);
            }

            query = query
                .Include(t => t.User)
                .Include(t => t.Tags);

            return query.ToPagedList(page, pageSize);
        }



        public async Task IncreaseViewCountAsync(Topic topic)
        {
            topic.ViewedCount++;

            await _dbContext.SaveChangesAsync();

        }


        public async Task IncreaseReplyCountAsync(long topicId)
        {
            var topic = await _dbContext.Topics.FindAsync(topicId);
            if (topic != null)
            {
                topic.ReplyedCount++;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task MakeAsDeletedAsync(long topicId)
        {
            var topic = await _dbContext.Topics.FindAsync(topicId);
            if (topic != null)
            {
                topic.Deleted = true;
                await _dbContext.SaveChangesAsync();
            }
        }


    }
}
