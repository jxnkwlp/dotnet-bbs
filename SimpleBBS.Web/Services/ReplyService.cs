using Microsoft.EntityFrameworkCore;
using SimpleBBS.Core;
using SimpleBBS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBBS.Web.Services
{
    public class ReplyService
    {
        private readonly ApplicationDbContext _dbContext;

        public ReplyService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

        }


        public async Task AddAsync(Reply reply)
        {
            await _dbContext.AddAsync(reply);
            await _dbContext.SaveChangesAsync();
        }

        public Task<List<Reply>> GetListByTopicIdAsync(long id)
        {
            return _dbContext.Reply
                .Where(t => t.TopicId == id)
                .OrderBy(t => t.CreationTime)
                .ThenBy(t => t.Id)
                .Include(t => t.User)
                .ToListAsync();
        }

        public IList<Reply> GetLastByTopicIds(params long[] ids)
        {
            //var list = from q in _dbContext.Reply 
            //           where ids.Contains(q.TopicId) 
            //           group q by q.CreationTime into g
            //           select new { g.Key, result = g.OrderByDescending(t => t.CreationTime).FirstOrDefault() };
            // return list.Select(t => t.result).ToList();

            return _dbContext.Reply
                  .Where(t => ids.Contains(t.TopicId)).ToList();

            // TODO 
            //var list = _dbContext.Reply
            //      .Where(t => ids.Contains(t.TopicId))
            //      .Include(t => t.User)
            //      .GroupBy(t => t)
            //      .Select(t => t.OrderByDescending(o => o.CreationTime).FirstOrDefault());

            //return list.ToList();
        }

    }
}
