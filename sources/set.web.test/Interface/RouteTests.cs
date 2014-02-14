using NUnit.Framework;

using set.web.test.Shared;

namespace set.web.test.Interface
{
    [TestFixture]
    public class RouteTests : BaseInterfaceTest
    {
        [TestCase(ACTION_HOME),
         TestCase(ACTION_CONTACT),

         TestCase(ACTION_LOGIN),
         TestCase(ACTION_SIGNUP),
         TestCase(ACTION_PASSWORD_RESET),
         ]
        public void should_view(string view)
        {
            var url = string.Format("{0}{1}", BASE_URL, view);

            GoTo(url);
            AssertUrl(url);

            CloseBrowser();
        }

        [TestCase(ACTION_USER_PROFILE),
        
         TestCase(ACTION_NEW_DOMAIN_OBJECT),
         TestCase(ACTION_LIST_DOMAIN_OBJECTS),
        TestCase(ACTION_DETAIL_DOMAIN_OBJECTS)]
        public void should_view_after_login_as_user(string view)
        {
            LoginAsUser();

            var url = string.Format("{0}{1}", BASE_URL, view);

            GoTo(url);
            AssertUrl(url);

            CloseBrowser();
        }

        [TestCase(ACTION_ADMIN_USER_LISTING),
         TestCase(ACTION_ADMIN_FEEDBACK_LISTING),
         TestCase(ACTION_ADMIN_CONTACT_MESSAGES_LISTING)]
        public void should_view_after_login_as_admin(string view)
        {
            LoginAsAdmin();

            var url = string.Format("{0}{1}", BASE_URL, view);

            GoTo(url);
            AssertUrl(url);

            CloseBrowser();
        }

        [TestCase(ACTION_ADMIN_USER_LISTING),
         TestCase(ACTION_ADMIN_FEEDBACK_LISTING),
         TestCase(ACTION_ADMIN_CONTACT_MESSAGES_LISTING)]
        public void should_redirect_to_home_if_not_admin(string view)
        {
            LoginAsUser();

            var url = string.Format("{0}{1}", BASE_URL, view);
            var homeUrl = string.Format("{0}{1}", BASE_URL, ACTION_HOME);

            GoTo(url);

            Assert.IsNotNull(Browser);
            Assert.AreEqual(Browser.Url, homeUrl);

            CloseBrowser();
        }
    }
}