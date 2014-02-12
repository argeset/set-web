using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using set.web.Data.Services;
using set.web.Helpers;
using set.web.Models;
using IFormsAuthenticationService = set.web.Configurations.IFormsAuthenticationService;

namespace set.web.Controllers
{
    public class UserController : BaseController
    {
        private readonly IFormsAuthenticationService _formsAuthenticationService;
        private readonly IUserService _userService;

        public UserController(IFormsAuthenticationService formsAuthenticationService, IUserService userService)
        {
            _formsAuthenticationService = formsAuthenticationService;
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
            if (!model.IsValidForNewDeveloper())
            {
                model.Msg = "bir sorun oluştu";
                return View(model);
            }

            model.Language = Thread.CurrentThread.CurrentUICulture.Name;
            var status = await _userService.Create(model, ConstHelper.User);
            if (!status)
            {
                model.Msg = "bir sorun oluştu";
                return View(model);
            }

            _formsAuthenticationService.SignIn(model.Id, model.Name, model.Email, ConstHelper.User, true);

            return Redirect("/user/apps");
        }

        [HttpGet, AllowAnonymous]
        public ActionResult Reset()
        {
            var model = new ResetModel();

            if (User.Identity.IsAuthenticated)
            {
                model.Email = User.Identity.GetEmail();
            }

            return View(model);
        }

        [HttpGet, AllowAnonymous]
        public ActionResult Login()
        {
            return View(new LoginModel());
        }

        [HttpPost, ValidateAntiForgeryToken, AllowAnonymous]
        public async Task<ActionResult> Login(LoginModel model)
        {

            if (!model.IsValid())
            {
                //todo: locale stringler gelecek
                model.Msg = "Bilgileri eksiksiz doldurunuz.";
                return View(model);
            }

            var authenticated = await _userService.Authenticate(model.Email, model.Password);
            if (!authenticated)
            {
                //todo: locale stringler gelecek
                model.Msg = "Hatalı email yada parola";
                return View(model);
            }

            var user = await _userService.GetByEmail(model.Email);
            _formsAuthenticationService.SignIn(user.Id, user.Name, user.Email, ConstHelper.User, true);

            return Redirect(!string.IsNullOrEmpty(model.ReturnUrl) ? model.ReturnUrl : "/");
        }

        [HttpGet]
        public ActionResult Logout()
        {
            _formsAuthenticationService.SignOut();
            return RedirectToHome();
        }
    }
}