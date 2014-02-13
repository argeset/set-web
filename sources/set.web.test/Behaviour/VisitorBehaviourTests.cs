﻿using System.Threading.Tasks;
using System.Web.Mvc;

using Moq;
using NUnit.Framework;

using set.web.Data.Services;
using set.web.Helpers;
using set.web.Models;
using set.web.test.Shared;
using set.web.test.Shared.Builders;

namespace set.web.test.Behaviour
{
    [TestFixture]
    public class VisitorBehaviourTests : BaseBehaviourTest
    {
        [Test]
        public async void any_visitor_can_create_user_account()
        {
            //arrange 
            var userService = new Mock<IUserService>();
            userService.Setup(x => x.Create(ValidUser, ConstHelper.User))
                       .Returns(Task.FromResult(true));

            var authService = new Mock<IAuthService>();
            authService.Setup(x => x.SignIn(ValidUser.Id, ValidUser.Name, ValidUser.Email, ConstHelper.User, true));

            //act
            var sut = new UserControllerBuilder().WithUserService(userService.Object)
                                                 .WithAuthService(authService.Object)
                                                 .Build();

            var result = await sut.New(ValidUser);

            ////assert
            Assert.IsNotNull(result);
            Assert.IsAssignableFrom<RedirectResult>(result);

            userService.Verify(x => x.Create(ValidUser, ConstHelper.User), Times.Once);
            authService.Verify(x => x.SignIn(ValidUser.Id, ValidUser.Name, ValidUser.Email, ConstHelper.User, true), Times.Once);

            sut.AssertPostAttribute(ACTION_NEW, new[] { typeof(UserModel) });
            sut.AssertAllowAnonymousAttribute(ACTION_NEW, new[] { typeof(UserModel) });
        }

        [Test]
        public async void any_visitor_can_send_feedback()
        {
            //arrange 
            var feedbackService = new Mock<IFeedbackService>();
            feedbackService.Setup(x => x.CreateFeedback(ValidContactMessage.Message, ValidContactMessage.Email))
                           .Returns(Task.FromResult(true));

            //act
            var sut = new FeedbackControllerBuilder().WithFeedbackService(feedbackService.Object)
                                                     .BuildWithMockControllerContext(ValidUser.Id, ValidUser.Name, ValidUser.Email, ValidUser.RoleName);

            var result = await sut.New(ValidContactMessage.Message);

            //assert
            Assert.IsNotNull(result);
            Assert.IsAssignableFrom<JsonResult>(result);

            feedbackService.Verify(x => x.CreateFeedback(ValidContactMessage.Message, ValidContactMessage.Email), Times.Once);

            sut.AssertPostAttributeWithOutAntiForgeryToken(ACTION_NEW, new[] { typeof(string) });
            sut.AssertAllowAnonymousAttribute(ACTION_NEW, new[] { typeof(string) });
        }

        [Test]
        public async void any_visitor_can_send_contact_message()
        {
            //arrange 
            var feedbackService = new Mock<IFeedbackService>();
            feedbackService.Setup(x => x.CreateContactMessage(ValidContactMessage.Subject, ValidContactMessage.Email, ValidContactMessage.Message))
                           .Returns(Task.FromResult(true));

            //act
            var sut = new HomeControllerBuilder().WithFeedbackService(feedbackService.Object)
                                                 .Build();
            var result = await sut.Contact(ValidContactMessage);

            //assert
            Assert.IsNotNull(result);
            Assert.IsAssignableFrom<ViewResult>(result);

            feedbackService.Verify(x => x.CreateContactMessage(ValidContactMessage.Subject, ValidContactMessage.Email, ValidContactMessage.Message), Times.Once);

            sut.AssertPostAttribute(ACTION_CONTACT, new[] { typeof(ContactMessageModel) });
            sut.AssertAllowAnonymousAttribute(ACTION_CONTACT, new[] { typeof(ContactMessageModel) });
        }

        public void any_visitor_can_search() { }
    }
}