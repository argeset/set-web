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
         TestCase(ACTION_LIST_DOMAIN_OBJECTS)]
        public void should_view_after_login(string view)
        {
            LoginAsUser();

            var url = string.Format("{0}{1}", BASE_URL, view);

            GoTo(url);
            AssertUrl(url);

            CloseBrowser();
        }
    }
}