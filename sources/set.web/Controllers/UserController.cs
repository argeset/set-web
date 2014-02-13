using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

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
            if (!model.IsValidForNewDeveloper())
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
        public ActionResult Reset()
        {
            var model = new ResetModel();

            if (User.Identity.IsAuthenticated)
            {
                model.Email = User.Identity.GetEmail();
            }

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, AllowAnonymous]
        public async Task<ActionResult> Reset(ResetModel model)
        {

            if (!model.IsValid())
            {
                SetPleaseTryAgain(model);
                return View(model);
            }

            var user = await _userService.GetByEmail(model.Email);
            //todo: parola sıfırlama maili gönderilir.

            return Redirect("/user/login");
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