using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SimpleBBS.Web.Services;
using SimpleBBS.Web.Models.Users;
using SimpleBBS.Core;

namespace SimpleBBS.Web.Controllers
{
    [Authorize]
    public class UserController : WebBaseController
    {

        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;

        }


        public IActionResult Info()
        {
            return View();
        }


        public async Task<IActionResult> Setting()
        {
            var userId = User.Identity.GetUserId<long>();

            var user = await _userService.FindByIdAsync(userId.ToString());
            var userInfo = await _userService.GetUserInfoAsync(userId);

            var model = new SettingViewModel()
            {
                UserName = user.UserName,
                Email = user.Email,

                City = userInfo?.City,
                GitHubId = userInfo?.GitHubId,
                SiteUrl = userInfo?.SiteUrl,
                UserSign = userInfo?.UserSign,
                WeiboId = userInfo?.WeiboId,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Setting(SettingViewModel model)
        {
            var userId = User.Identity.GetUserId<long>();

            try
            {
                await _userService.UpdateUserInfoAsync(new UserInfo()
                {
                    UserId = userId,

                    City = model.City,
                    GitHubId = model.GitHubId,
                    SiteUrl = model.SiteUrl,
                    UserSign = model.UserSign,
                    WeiboId = model.WeiboId,
                });

                ShowMessage(true, "修改信息成功");
            }
            catch (Exception)
            {
                ShowMessage(false, "保存失败");
            }

            return View(model);
        }


        public async Task<IActionResult> ChangePassword()
        {

            return await Setting();
        }

    }
}