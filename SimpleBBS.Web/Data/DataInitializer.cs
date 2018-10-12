using Microsoft.EntityFrameworkCore;
using SimpleBBS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBBS.Web.Data
{
    public class DataInitializer
    {
        private readonly ApplicationDbContext _dbContext;

        public DataInitializer(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Initialize()
        {
            _dbContext.Database.Migrate();

            if (!_dbContext.Users.Any())
            {
                var user = new Core.User()
                {
                    UserName = "123@qq.com",
                    Email = "123@qq.com",
                    PasswordHash = "AQAAAAEAACcQAAAAEKd67wJ+Mvii2qP2J2qfYCRVfLRSNjNWwwD5jNO9KwalYfViNcvxp9H6gFnN+Jrcfg==",
                    SecurityStamp = "HX43PJ5IFBQAVOUGSTGOMFYGKCXBDP7H",
                    ConcurrencyStamp = "42b0cdd2-ebcb-4f52-8c0b-97b69171bf9f"
                };

                user.NormalizedUserName = user.UserName.ToUpperInvariant();
                user.NormalizedEmail = user.Email.ToUpperInvariant();

                _dbContext.Users.Add(user);

                _dbContext.SaveChanges();
            }


            if (!_dbContext.Tags.Any())
            {
                _dbContext.Tags.Add(new Core.Tags() { Name = "分享" });
                _dbContext.Tags.Add(new Core.Tags() { Name = "问答" });
                _dbContext.Tags.Add(new Core.Tags() { Name = "招聘" });
                _dbContext.Tags.Add(new Core.Tags() { Name = "测试1" });
                _dbContext.Tags.Add(new Core.Tags() { Name = "测试2" });

                _dbContext.SaveChanges();
            }

        }
    }
}
