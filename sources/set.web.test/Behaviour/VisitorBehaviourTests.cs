using System;
using System.Threading;
using System.Threading.Tasks;
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
    public class VisitorBehaviourTests
    {
        private const string ActionNameNew = "New";
        private const string ActionNameContact = "Contact";

        public void any_visitor_can_give_feedback()
        {

        }

        [Test]
        public async void any_visitor_can_create_user_account()
        {
            //arrange
            var validModel = new UserModel
            {
                Name = "name",
                Password = "pass",
                Email = "test@test.com",
                Language = Thread.CurrentThread.CurrentUICulture.Name,
                Id = Guid.NewGuid().ToNoDashString()
            };

            var userService = new Mock<IUserService>();
            userService.Setup(x => x.Create(validModel, ConstHelper.User))
                       .Returns(Task.FromResult(true));

            var authService = new Mock<IAuthService>();
            authService.Setup(x => x.SignIn(validModel.Id, validModel.Name, validModel.Email, ConstHelper.User, true));

            //act
            var sut = new UserControllerBuilder().WithUserService(userService.Object)
                                                 .WithAuthService(authService.Object)
                                                 .Build();

            var result = await sut.New(validModel);

            ////assert
            Assert.IsNotNull(result);
            Assert.IsAssignableFrom<RedirectResult>(result);

            userService.Verify(x => x.Create(validModel, ConstHelper.User), Times.Once);
            authService.Verify(x => x.SignIn(validModel.Id, validModel.Name, validModel.Email, ConstHelper.User, true), Times.Once);

            sut.AssertPostAttribute(ActionNameNew, new[] { typeof(UserModel) });
            sut.AssertAllowAnonymousAttribute(ActionNameNew, new[] { typeof(UserModel) });
        }

        [Test]
        public async void any_visitor_can_send_feedback()
        {
            //arrange
            var validModel = new ContactMessageModel
            { 
                Message = "message",
                Email = "test@test.com"
            };

            var feedbackService = new Mock<IFeedbackService>();
            feedbackService.Setup(x => x.CreateFeedback(validModel.Message, validModel.Email))
                           .Returns(Task.FromResult(true));

            //act
            var sut = new FeedbackControllerBuilder().WithFeedbackService(feedbackService.Object)
                                                 .BuildWithMockControllerContext("1","name","test@test.com",ConstHelper.User);
            var result = await sut.New(validModel.Message);

            //assert
            Assert.IsNotNull(result);
            Assert.IsAssignableFrom<JsonResult>(result);

            feedbackService.Verify(x => x.CreateFeedback(validModel.Message, validModel.Email), Times.Once);

            sut.AssertPostAttributeWithOutAntiForgeryToken(ActionNameNew, new[] { typeof(string) });
            sut.AssertAllowAnonymousAttribute(ActionNameNew, new[] { typeof(string) }); 
        }

        [Test]
        public async void any_visitor_can_send_contact_message()
        {
            //arrange
            var validModel = new ContactMessageModel
            {
                Subject = "subject",
                Message = "message",
                Email = "test@test.com"
            };

            var feedbackService = new Mock<IFeedbackService>();
            feedbackService.Setup(x => x.CreateContactMessage(validModel.Subject,validModel.Email,validModel.Message))
                           .Returns(Task.FromResult(true));

            //act
            var sut = new HomeControllerBuilder().WithFeedbackService(feedbackService.Object)
                                                 .Build();
            var result = await sut.Contact(validModel);

            //assert
            Assert.IsNotNull(result); 
            Assert.IsAssignableFrom<ViewResult>(result);

            feedbackService.Verify(x => x.CreateContactMessage(validModel.Subject, validModel.Email, validModel.Message), Times.Once);

            sut.AssertPostAttribute(ActionNameContact, new[] { typeof(ContactMessageModel) });
            sut.AssertAllowAnonymousAttribute(ActionNameContact, new[] { typeof(ContactMessageModel) });

        }

        public void any_visitor_can_search_meta_data() { }
    }
}