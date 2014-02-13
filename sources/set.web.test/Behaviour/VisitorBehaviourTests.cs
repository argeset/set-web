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
    public class VisitorBehaviourTests : BaseBehaviourTest
    { 
        public void any_visitor_can_give_feedback()
        {

        }

        [Test]
        public async void any_visitor_can_create_user_account()
        {
            //arrange 
            var userService = new Mock<IUserService>();
            userService.Setup(x => x.Create(VALID_USER_MODEL, ConstHelper.User))
                       .Returns(Task.FromResult(true));

            var authService = new Mock<IAuthService>();
            authService.Setup(x => x.SignIn(VALID_USER_MODEL.Id, VALID_USER_MODEL.Name, VALID_USER_MODEL.Email, ConstHelper.User, true));

            //act
            var sut = new UserControllerBuilder().WithUserService(userService.Object)
                                                 .WithAuthService(authService.Object)
                                                 .Build();

            var result = await sut.New(VALID_USER_MODEL);

            ////assert
            Assert.IsNotNull(result);
            Assert.IsAssignableFrom<RedirectResult>(result);

            userService.Verify(x => x.Create(VALID_USER_MODEL, ConstHelper.User), Times.Once);
            authService.Verify(x => x.SignIn(VALID_USER_MODEL.Id, VALID_USER_MODEL.Name, VALID_USER_MODEL.Email, ConstHelper.User, true), Times.Once);

            sut.AssertPostAttribute(ACTION_NEW, new[] { typeof(UserModel) });
            sut.AssertAllowAnonymousAttribute(ACTION_NEW, new[] { typeof(UserModel) });
        }

        [Test]
        public async void any_visitor_can_send_feedback()
        {
            //arrange 
            var feedbackService = new Mock<IFeedbackService>();
            feedbackService.Setup(x => x.CreateFeedback(VALID_CONTACT_MESSAGE_MODEL.Message, VALID_CONTACT_MESSAGE_MODEL.Email))
                           .Returns(Task.FromResult(true));

            //act
            var sut = new FeedbackControllerBuilder().WithFeedbackService(feedbackService.Object)
                                                     .BuildWithMockControllerContext("1","name","test@test.com",ConstHelper.User);
            var result = await sut.New(VALID_CONTACT_MESSAGE_MODEL.Message);

            //assert
            Assert.IsNotNull(result);
            Assert.IsAssignableFrom<JsonResult>(result);

            feedbackService.Verify(x => x.CreateFeedback(VALID_CONTACT_MESSAGE_MODEL.Message, VALID_CONTACT_MESSAGE_MODEL.Email), Times.Once);

            sut.AssertPostAttributeWithOutAntiForgeryToken(ACTION_NEW, new[] { typeof(string) });
            sut.AssertAllowAnonymousAttribute(ACTION_NEW, new[] { typeof(string) }); 
        }

        [Test]
        public async void any_visitor_can_send_contact_message()
        {
            //arrange 
            var feedbackService = new Mock<IFeedbackService>();
            feedbackService.Setup(x => x.CreateContactMessage(VALID_CONTACT_MESSAGE_MODEL.Subject,VALID_CONTACT_MESSAGE_MODEL.Email,VALID_CONTACT_MESSAGE_MODEL.Message))
                           .Returns(Task.FromResult(true));

            //act
            var sut = new HomeControllerBuilder().WithFeedbackService(feedbackService.Object)
                                                 .Build();
            var result = await sut.Contact(VALID_CONTACT_MESSAGE_MODEL);

            //assert
            Assert.IsNotNull(result); 
            Assert.IsAssignableFrom<ViewResult>(result);

            feedbackService.Verify(x => x.CreateContactMessage(VALID_CONTACT_MESSAGE_MODEL.Subject, VALID_CONTACT_MESSAGE_MODEL.Email, VALID_CONTACT_MESSAGE_MODEL.Message), Times.Once);

            sut.AssertPostAttribute(ACTION_CONTACT, new[] { typeof(ContactMessageModel) });
            sut.AssertAllowAnonymousAttribute(ACTION_CONTACT, new[] { typeof(ContactMessageModel) });
        }

        public void any_visitor_can_search_domain_object() { }
    }
}