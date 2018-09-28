using SimpleBBS.Core;
using SimpleBBS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBBS.Web.Services
{
    public class TagsService
    {
        private readonly ApplicationDbContext _dbContext;

        public TagsService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

        }


        public IList<Tags> GetList()
        {
            return _dbContext.Tags.ToList();
        }

    }
}
