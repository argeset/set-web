using NUnit.Framework;
using set.web.test.Shared;

namespace set.web.test.Interface
{
    [TestFixture]
    public class RouteTests : BaseInterfaceTest
    {
        [Test]
        public void should_view_home_index()
        {
            var url = string.Format("{0}{1}", BASE_URL, ACTION_HOME);

            GoTo(url);
            AssertUrl(url);

            CloseBrowser();
        }
        
        [Test]
        public void should_view_home_contact()
        {
            var url = string.Format("{0}{1}", BASE_URL, ACTION_CONTACT);

            GoTo(url);
            AssertUrl(url);

            CloseBrowser();
        }
       
    }
}