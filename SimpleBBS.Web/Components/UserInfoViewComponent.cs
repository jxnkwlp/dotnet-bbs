﻿using Microsoft.AspNetCore.Mvc;
using SimpleBBS.Web.Models;
using SimpleBBS.Web.Models.Users;
using SimpleBBS.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBBS.Web.Components
{
    public class UserInfoViewComponent : ViewComponent
    {
        private readonly UserService _userService;

        public UserInfoViewComponent(UserService userService)
        {
            _userService = userService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int userId)
        {
            var user = await _userService.GetUserFullInfoAsync(userId);

            var vm = user.ToModel();

            return View(vm);
        }
    }
}
