using NUnit.Framework;
using OpenQA.Selenium.Firefox;

namespace set.web.test.Shared
{
    public class BaseInterfaceTest
    {
        public const string BASE_URL = "http://localhost:8011";

        public const string ACTION_HOME = "/home/index";
        public const string ACTION_CONTACT = "/home/contact";

        public const string ACTION_LOGIN = "/user/login";
        public const string ACTION_LOGOUT = "/user/logout";
        public const string ACTION_SIGNUP = "/user/new";
        public const string ACTION_PASSWORD_RESET = "/user/reset";
        public const string ACTION_USER_PROFILE = "/user/detail";

        public const string ACTION_NEW_DOMAIN_OBJECT = "/domainobject/new";
        public const string ACTION_LIST_DOMAIN_OBJECTS = "/domainobject/list";

        public FirefoxDriver Browser;

        [SetUp]
        public void Setup()
        {
            Browser = new FirefoxDriver();
        }

        public void LoginAsUser()
        {
            LogOut();

            Browser.FindElementById("Email").SendKeys("user@test.com");
            Browser.FindElementById("Password").SendKeys("password");
            Browser.FindElementById("frm").Submit();
        }

        public void LogOut()
        {
            GoTo(string.Format("{0}{1}", BASE_URL, ACTION_LOGOUT));
        }

        public void GoTo(string url)
        {
            Browser.Navigate().GoToUrl(url);
        }

        public void AssertUrl(string url)
        {
            Assert.IsNotNull(Browser);
            Assert.AreEqual(Browser.Url, url);
        }

        public void CloseBrowser()
        {
            Browser.Close();
        }
    }
}