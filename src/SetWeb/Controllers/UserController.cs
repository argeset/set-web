using System.Web.Mvc;
using SetWeb.Services;

namespace SetWeb.Controllers
{
    public class UserController: Controller
    {
        private readonly IFormsAuthenticationService _formsAuthenticationService;

        public UserController(IFormsAuthenticationService formsAuthenticationService)
        {
            _formsAuthenticationService = formsAuthenticationService;
        }

        [HttpGet]
        public ActionResult Logout()
        {
            _formsAuthenticationService.SignOut();
            return Redirect("/");
        }
    }
}