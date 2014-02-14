using System;
using NUnit.Framework;

using set.web.test.Shared;

namespace set.web.test.Interface
{
    [TestFixture]
    public class FormTests : BaseInterfaceTest
    {
        [Test]
        public void should_save_new_feedback_via_popup_form()
        {
            var homeUrl = string.Format("{0}{1}", BASE_URL, ACTION_HOME);

            GoTo(homeUrl);

            Browser.FindElementById("btnOpenFeedBack").Click();
            Browser.FindElementById("FeedbackMessage").SendKeys("test feedback");
            Browser.FindElementById("btnSaveFeedback").Click();

            Browser.Manage().Timeouts().SetScriptTimeout(TimeSpan.FromSeconds(5));

            Assert.IsFalse(Browser.FindElementById("modalFeedback").Displayed);

            CloseBrowser();
        }
    }
}