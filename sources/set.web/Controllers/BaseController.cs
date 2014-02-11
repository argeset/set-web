using System.Threading;
using System.Web.Mvc;

using set.web.Helpers;
using set.web.Models;

namespace set.web.Controllers
{
    [AllowAnonymous]
    public class BaseController : Controller
    {
        public HtmlHelper SetHtmlHelper;

        public BaseController()
        {
            SetHtmlHelper = new HtmlHelper(new ViewContext(), new ViewPage());
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            SetLanguage();

            base.OnActionExecuting(filterContext);
        }

        public void SetLanguage()
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = ConstHelper.CultureEN;
                Thread.CurrentThread.CurrentUICulture = ConstHelper.CultureEN;

                var langCookie = Request.Cookies[ConstHelper.__Lang];
                if (langCookie == null) return;

                var lang = langCookie.Value;
                if (lang != ConstHelper.tr) return;

                Thread.CurrentThread.CurrentCulture = ConstHelper.CultureTR;
                Thread.CurrentThread.CurrentUICulture = ConstHelper.CultureTR;
            }
            catch { }
        }

        public RedirectResult RedirectToHome()
        {
            return Redirect("/");
        }

        public void SetPleaseTryAgain(BaseModel model)
        {
            model.Msg = SetHtmlHelper.LocalizationString("please_check_the_fields_and_try_again");
        }
    }
}