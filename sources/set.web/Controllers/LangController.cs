﻿using System.Web;
using System.Web.Mvc;

using set.web.Helpers;

namespace set.web.Controllers
{
    public class LangController : BaseController
    {
        [HttpGet, AllowAnonymous]
        public ActionResult Change(string id)
        {
            Response.SetCookie(new HttpCookie(ConstHelper.__Lang, id));

            return HttpContext.Request.UrlReferrer != null ? Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri) : RedirectToHome();
        }
    }
}