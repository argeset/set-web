﻿using System.Web.Mvc;
using System.Threading;
using System.Threading.Tasks;

using set.web.Data.Services;
using set.web.Helpers;
using set.web.Models;

namespace set.web.Controllers
{
    public class UserController : BaseController
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public UserController(
            IAuthService authService,
            IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpGet, AllowAnonymous]
        public ActionResult New()
        {
            return View(new UserModel());
        }

        [HttpPost, ValidateAntiForgeryToken, AllowAnonymous]
        public async Task<ActionResult> New(UserModel model)
        {
            if (!model.IsValid())
            {
                SetPleaseTryAgain(model);
                return View(model);
            }

            model.Language = Thread.CurrentThread.CurrentUICulture.Name;
            var status = await _userService.Create(model, ConstHelper.User);
            if (!status)
            {
                SetPleaseTryAgain(model);
                return View(model);
            }

            _authService.SignIn(model.Id, model.Name, model.Email, ConstHelper.User, true);

            return Redirect("/");
        }

        [HttpGet, AllowAnonymous]
        public ActionResult PasswordReset()
        {
            var model = new PasswordResetModel();

            if (User.Identity.IsAuthenticated)
            {
                model.Email = User.Identity.GetEmail();
            }

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, AllowAnonymous]
        public async Task<ActionResult> PasswordReset(PasswordResetModel model)
        {
            SetPleaseTryAgain(model);
            if (model.IsNotValid())
            {
                return View(model);
            }

            var isOk = await _userService.RequestPasswordReset(model.Email);
            if (isOk)
            {
                model.Msg = SetHtmlHelper.LocalizationString("password_reset_request_successful");
            }

            return View(model);
        }

        [HttpGet, AllowAnonymous]
        public async Task<ActionResult> PasswordChange(string email, string token)
        {
            var model = new PasswordChangeModel { Email = email, Token = token };
            SetPleaseTryAgain(model);
            if (model.IsNotValid())
            {
                return View(model);
            }

            if (await _userService.IsPasswordResetRequestValid(model.Email, model.Token))
            {
                return Redirect("/User/Login");
            }

            return View(model);
        }

        [HttpPost, AllowAnonymous]
        public async Task<ActionResult> PasswordChange(PasswordChangeModel model)
        {
            SetPleaseTryAgain(model);
            if (model.IsNotValid())
            {
                return View(model);
            }

            if (!await _userService.ChangePassword(model.Email, model.Token, model.Password))
            {
                return View(model);
            }

            return Redirect("/User/Login");
        }

        [HttpGet, AllowAnonymous]
        public ActionResult Login()
        {
            return View(new LoginModel());
        }

        [HttpPost, ValidateAntiForgeryToken, AllowAnonymous]
        public async Task<ActionResult> Login(LoginModel model)
        {
            SetPleaseTryAgain(model);

            if (!model.IsValid())
            {
                return View(model);
            }

            var authenticated = await _userService.Authenticate(model.Email, model.Password);
            if (!authenticated) return View(model);

            var user = await _userService.GetByEmail(model.Email);
            _authService.SignIn(user.Id, user.Name, user.Email, ConstHelper.User, true);

            return Redirect(!string.IsNullOrEmpty(model.ReturnUrl) ? model.ReturnUrl : "/");
        }

        [HttpGet]
        public ActionResult Logout()
        {
            _authService.SignOut();
            return RedirectToHome();
        }
    }
}