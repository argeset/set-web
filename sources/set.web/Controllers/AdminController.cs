using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using set.web.Data.Entities;
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
            {
                //filterContext.Result = RedirectToHome();
            }

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
        public async Task<ActionResult> Users(int id = 0, int page = 1)
        {
            var pageNumber = page;
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }

            PagedList<User> users;

            ViewBag.RoleId = id;
            if (SetLocaleRole.IsValid(id))
            {
                users = await _userService.GetAllByRoleId(id, pageNumber);
            }
            else
            {
                users = await _userService.GetUsers(pageNumber);
            }

            var list = users.Items.Select(UserModel.Map).ToList();

            var model = new PageModel<UserModel>
            {
                Items = list,
                HasNextPage = users.HasNextPage,
                HasPreviousPage = users.HasPreviousPage,
                Number = users.Number,
                TotalCount = users.TotalCount,
                TotalPageCount = users.TotalPageCount
            };

            return View(model);
        }

<<<<<<< HEAD
=======
        [HttpGet]
        public async Task<ViewResult> Feedbacks(int id = 1)
        {
            var result = await _feedbackService.GetFeedbacks(id);
            var list = result.Items.Select(FeedbackModel.Map).ToList();
            var model = new PageModel<FeedbackModel>
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

>>>>>>> 1282c2b7d8d6b49e7b0d3216faf7df626f6aaf1f
    }
}