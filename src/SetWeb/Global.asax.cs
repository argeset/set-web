using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using Castle.Windsor;
using Castle.Windsor.Installer;

using SetWeb.Configurations;
using SetWeb.Helpers;

namespace SetWeb
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            MvcHandler.DisableMvcResponseHeader = true;

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            PrepareIocContainer();

            PrepareLocalizationStrings();
        }

        private void PrepareLocalizationStrings()
        {
            var enTexts = new Dictionary<string, string>();
            var trTexts = new Dictionary<string, string>();

           

            Application.Add(ConstHelper.LocalizationStringsEN, enTexts);
            Application.Add(ConstHelper.LocalizationStringsTR, trTexts);
        }

        private static void PrepareIocContainer()
        {
            var container = new WindsorContainer().Install(FromAssembly.This());
            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(container.Kernel));
        }

        protected void Application_PreSendRequestHeaders(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Headers.Remove("X-Powered-By");
            HttpContext.Current.Response.Headers.Remove("X-AspNet-Version");
            HttpContext.Current.Response.Headers.Remove("X-AspNetMvc-Version");

            HttpContext.Current.Response.Headers.Set("Server", string.Format("Web Server ({0}) ", Environment.MachineName));
        }
    }
}