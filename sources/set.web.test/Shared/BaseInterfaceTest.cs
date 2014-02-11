using System;
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
        public const string ACTION_PROFILE = "/user/detail";


        public FirefoxDriver Browser;

        [SetUp]
        public void Setup()
        {
            Browser = new FirefoxDriver();
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