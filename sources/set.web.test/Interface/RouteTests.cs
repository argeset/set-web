using NUnit.Framework;
using set.web.test.Shared;

namespace set.web.test.Interface
{
    [TestFixture]
    public class RouteTests : BaseInterfaceTest
    {
        [TestCase(ACTION_HOME),
         TestCase(ACTION_CONTACT),
         TestCase(ACTION_LOGOUT),
         TestCase(ACTION_SIGNUP)]
        public void should_view(string view)
        {
            var url = string.Format("{0}{1}", BASE_URL, view);

            GoTo(url);
            AssertUrl(url);

            CloseBrowser();
        }
    }
}