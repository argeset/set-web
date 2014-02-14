﻿using System;
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

        [Test]
        public void domainobject_save_and_new_after_only_save_form()
        {
            LoginAsUser();

            var url = string.Format("{0}{1}", BASE_URL, ACTION_NEW_DOMAIN_OBJECT);
            var domainObjListUrl = string.Format("{0}{1}", BASE_URL, ACTION_LIST_DOMAIN_OBJECTS);

            GoTo(url);

            Browser.FindElementById("Name").SendKeys("test domain obj with save and new");
            Browser.FindElementById("btnSaveAndNew").Click();

            Assert.IsNotNull(Browser);
            Assert.AreEqual(Browser.Url, url);

            Browser.FindElementById("Name").SendKeys("test domain obj with only save");
            Browser.FindElementById("btnSave").Click();

            Assert.IsNotNull(Browser);
            Assert.AreEqual(Browser.Url, domainObjListUrl);

            CloseBrowser();
        }
    }
}