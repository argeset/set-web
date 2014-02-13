using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;

using set.web.Configurations;
using set.web.Data.Entities;
using set.web.Data.Services;
using set.web.Helpers;
using set.web.Models;
using set.web.test.Shared;
using set.web.test.Shared.Builders;

namespace set.web.test.Behaviour
{
    [TestFixture]
    public class MembershipBehaviourTests
    {
        private const string ActionNameLogin = "Login";
        private const string ActionNameLogout = "Logout";
        private const string ActionNameReset = "PasswordReset";
          
        [Test]
        public async void any_user_can_login()
        {
            //arrange
            var validModel = new LoginModel
            {
                Password = "pass",
                Email = "test@test.com"
            };

            var user = new User
            {
                Id = "1",
                Name = "name",
                Email = "test@test.com",
                RoleName = ConstHelper.User
            };

            var userService = new Mock<IUserService>();
            userService.Setup(x => x.Authenticate(validModel.Email, validModel.Password))
                       .Returns(Task.FromResult(true));

            userService.Setup(x => x.GetByEmail(validModel.Email))
                       .Returns(Task.FromResult(user));

            var authService = new Mock<IAuthService>();
            authService.Setup(x => x.SignIn(user.Id, user.Name, user.Email, ConstHelper.User, true));

            ////act
            var sut = new UserControllerBuilder().WithUserService(userService.Object)
                                                 .WithAuthService(authService.Object)
                                                 .Build();

            var result = await sut.Login(validModel);

            ////assert
            Assert.IsNotNull(result);
            Assert.IsAssignableFrom<RedirectResult>(result);

            userService.Verify(x => x.Authenticate(validModel.Email, validModel.Password), Times.Once);
            userService.Verify(x => x.GetByEmail(validModel.Email), Times.Once);
            authService.Verify(x => x.SignIn(user.Id, user.Name, user.Email, ConstHelper.User, true), Times.Once);

            sut.AssertPostAttribute(ActionNameLogin, new[] { typeof(LoginModel) });
            sut.AssertAllowAnonymousAttribute(ActionNameLogin, new[] { typeof(LoginModel) });
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