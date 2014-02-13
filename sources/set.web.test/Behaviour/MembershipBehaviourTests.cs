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
    public class MembershipBehaviourTests : BaseBehaviourTest
    { 
        [Test]
        public async void any_user_can_login()
        {
            //arrange  
            var userService = new Mock<IUserService>();
            userService.Setup(x => x.Authenticate(VALID_LOGIN_MODEL.Email, VALID_LOGIN_MODEL.Password))
                       .Returns(Task.FromResult(true));

            userService.Setup(x => x.GetByEmail(VALID_LOGIN_MODEL.Email))
                       .Returns(Task.FromResult(VALID_USER_ENTITY));

            var authService = new Mock<IAuthService>();
            authService.Setup(x => x.SignIn(VALID_USER_ENTITY.Id, VALID_USER_ENTITY.Name, VALID_USER_ENTITY.Email, ConstHelper.User, true));

            ////act
            var sut = new UserControllerBuilder().WithUserService(userService.Object)
                                                 .WithAuthService(authService.Object)
                                                 .Build();

            var result = await sut.Login(VALID_LOGIN_MODEL);

            ////assert
            Assert.IsNotNull(result);
            Assert.IsAssignableFrom<RedirectResult>(result);

            userService.Verify(x => x.Authenticate(VALID_LOGIN_MODEL.Email, VALID_LOGIN_MODEL.Password), Times.Once);
            userService.Verify(x => x.GetByEmail(VALID_LOGIN_MODEL.Email), Times.Once);
            authService.Verify(x => x.SignIn(VALID_USER_ENTITY.Id, VALID_USER_ENTITY.Name, VALID_USER_ENTITY.Email, ConstHelper.User, true), Times.Once);

            sut.AssertPostAttribute(ACTION_LOGIN, new[] { typeof(LoginModel) });
            sut.AssertAllowAnonymousAttribute(ACTION_LOGIN, new[] { typeof(LoginModel) });
        }

        [Test]
        public void any_user_can_logout()
        {
            //arrange
            var authService = new Mock<IAuthService>();
            authService.Setup(x => x.SignOut());

            //act
            var sut = new UserControllerBuilder().WithAuthService(authService.Object)
                                                 .Build();
            var result = sut.Logout();

            //assert
            Assert.IsNotNull(result);
            Assert.IsAssignableFrom<RedirectResult>(result);

            sut.AssertGetAttribute(ACTION_LOGOUT); 
        }

        [Test]
        public void any_user_can_request_password_reset_link()
        {
            //arrange

            //act

            //assert
        }

        public void any_user_can_change_password()
        {
            //arrange

            //act

            //assert
        }
    }
}