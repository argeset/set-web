using System.Threading.Tasks;
using System.Web.Mvc;

using set.web.Data.Services;
using set.web.Helpers;
using set.web.Models;

namespace set.web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IFeedbackService _feedbackService;

        public HomeController(
            IAuthService authService,
            IUserService userService,
            IFeedbackService feedbackService)
        {
            _authService = authService;
            _userService = userService;
            _feedbackService = feedbackService;

        }

        [HttpGet, AllowAnonymous]
        public async Task<ViewResult> Index()
        {
            if (!User.Identity.IsAuthenticated) return View();

            try
            {
                var id = User.Identity.GetId();
                if (string.IsNullOrEmpty(id))
                {
                    _authService.SignOut();
                }
                else
                {
                    var user = await _userService.Get(id);
                    if (user == null)
                    {
                        _authService.SignOut();
                    }
                }
            }
            catch
            {
                _authService.SignOut();
            }

            return View();
        }

        [HttpGet, AllowAnonymous]
        public ViewResult Contact()
        {
            var model = new ContactMessageModel();

            if (User.Identity.IsAuthenticated)
            {
                model.Email = User.Identity.GetEmail();
            }

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, AllowAnonymous]
        public async Task<ActionResult> Contact(ContactMessageModel model)
        {
            if (!model.IsValid())
            {
                SetPleaseTryAgain(model);
                return View(model);
            }

            model.IsOk = await _feedbackService.CreateContactMessage(model.Subject, model.Email, model.Message);
            if (model.IsOk)
            {
                model.Msg = "data_saved_successfully_msg".Localize();
            }

            return View(model);
        }
    }
}