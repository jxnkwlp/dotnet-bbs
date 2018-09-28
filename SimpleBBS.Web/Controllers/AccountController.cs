using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleBBS.Core;
using SimpleBBS.Web.Models.Accounts;
using SimpleBBS.Web.Services;

namespace SimpleBBS.Web.Controllers
{
    public class AccountController : WebBaseController
    {
        private readonly UserService _userservice;
        private readonly WebSignInManager _webSignInManager;

        public AccountController(UserService userService, WebSignInManager webSignInManager)
        {
            _userservice = userService;
            _webSignInManager = webSignInManager;

        }

        #region Login

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _webSignInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);

                if (result.Succeeded)
                {
                    return LocalRedirect("~/");
                }
                else
                {
                    ModelState.AddModelError("", "用户名或密码错误");
                }
            }

            return View(model);
        }

        #endregion

        #region Register

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.Email, Email = model.Email };
                var result = await _userservice.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var code = await _userservice.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, Request.Scheme);

                    //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                    //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    await _webSignInManager.SignInAsync(user, isPersistent: false);

                    return LocalRedirect("~/");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        #endregion


        #region ForgotPassword

        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View(new ForgotPasswordViewModel());
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Result = true;

                var user = await _userservice.FindByEmailAsync(model.Email);
                if (user != null && (await _userservice.IsEmailConfirmedAsync(user)))
                {
                    // For more information on how to enable account confirmation and password reset please 
                    // visit https://go.microsoft.com/fwlink/?LinkID=532713
                    var code = await _userservice.GeneratePasswordResetTokenAsync(user);
                    var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, Request.Scheme);

                    //await _emailSender.SendEmailAsync(
                    //    Input.Email,
                    //    "Reset Password",
                    //    $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
                }
            }

            return View(model);
        }
         
        #endregion

        #region ResetPassword

        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(long? userId, string code)
        {
            if (string.IsNullOrWhiteSpace(code) || userId == null)
                return Redirect("~/");

            var user = await _userservice.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                return Redirect("~/");
            }

            var model = new ResetPasswordViewModel()
            {
                UserId = userId.Value,
                Email = user.Email,
            };

            return View(model);
        }

        #endregion

    }
}