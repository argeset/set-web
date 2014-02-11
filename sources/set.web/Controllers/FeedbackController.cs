using System.Threading.Tasks;
using System.Web.Mvc;

using set.web.Data.Services;
using set.web.Helpers;
using set.web.Models;


namespace set.web.Controllers
{
    public class FeedbackController :BaseController
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpPost, AllowAnonymous]
        public async Task<JsonResult> New(string info)
        {
            var model = new ResponseModel { IsOk = false };
            SetPleaseTryAgain(model);

            if (string.IsNullOrWhiteSpace(info)) return Json(model, JsonRequestBehavior.DenyGet);

            var email = string.Empty;
            if (User.Identity.IsAuthenticated)
            {
                email = User.Identity.GetEmail();
            }

            model.IsOk = await _feedbackService.CreateFeedback(info, email);

            if (model.IsOk)
            {
                model.Msg = SetHtmlHelper.LocalizationString("data_saved_successfully_msg");
            }

            return Json(model, JsonRequestBehavior.DenyGet);
        }
    }
}