using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

using set.web.Configurations;
using set.web.Helpers;

namespace set.web.Controllers
{
    public class UserController  : BaseController
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
            return RedirectToHome();
        }
    }
}