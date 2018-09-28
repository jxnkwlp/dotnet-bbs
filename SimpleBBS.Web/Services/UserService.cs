using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SimpleBBS.Core;
using SimpleBBS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBBS.Web.Services
{
    public class UserService : UserManager<User>
    {
        private readonly ApplicationDbContext _dbContext;

        public UserService(IUserStore<User> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<User> passwordHasher, IEnumerable<IUserValidator<User>> userValidators, IEnumerable<IPasswordValidator<User>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<User>> logger, ApplicationDbContext dbContext) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            _dbContext = dbContext;
        }


        public async Task<UserInfo> GetUserInfoAsync(long userId)
        {
            return await _dbContext.FindAsync<UserInfo>(userId);
        }

        public async Task UpdateUserInfoAsync(UserInfo userInfo)
        {
            var entity = _dbContext.Find<UserInfo>(userInfo.UserId);

            if (entity != null)
            {
                entity.City = userInfo.City;
                entity.GitHubId = userInfo.GitHubId;
                entity.SiteUrl = userInfo.SiteUrl;
                entity.UserSign = userInfo.UserSign;
                entity.WeiboId = userInfo.WeiboId;

                _dbContext.Update(entity);
            }
            else
            {
                entity = userInfo;

                _dbContext.Add(entity);

            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
