using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

using set.web.Data.Services;
using set.web.Helpers;
using set.web.Models;

namespace set.web.Controllers
{
    public class AdminController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IFeedbackService _feedbackService;

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (User.Identity.GetRoleName() != ConstHelper.Admin)
                RedirectToHome();

            base.OnActionExecuting(filterContext);
        }

        public AdminController(IUserService userService, IFeedbackService feedbackService)
        {
            _userService = userService;
            _feedbackService = feedbackService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public async Task<ViewResult> Users(int id = 1)
        {
            var result = await _userService.GetUsers(id);
            var list = result.Items.Select(UserModel.Map).ToList();
            var model = new PageModel<UserModel>
            {
                Items = list,
                HasNextPage = result.HasNextPage,
                HasPreviousPage = result.HasPreviousPage,
                Number = result.Number,
                TotalCount = result.TotalCount,
                TotalPageCount = result.TotalPageCount
            };
            return View(model);
        }



    }
}