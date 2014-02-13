using System.Web;
using System.Web.Mvc;
using System.Security.Principal;

using Moq;
using set.web.Configurations;
using set.web.Controllers;
using set.web.Data.Services;

namespace set.web.test.Shared.Builders
{
    public class UserControllerBuilder : BaseBuilder
    {
        private IUserService _userService;
        private IAuthService _authService;

        public UserControllerBuilder()
        {
            _authService = null;
            _userService = null;
        }

        internal UserControllerBuilder WithAuthService(IAuthService authService)
        {
            _authService = authService;
            return this;
        }

        internal UserControllerBuilder WithUserService(IUserService userService)
        {
            _userService = userService;
            return this;
        }

        internal UserController BuildWithMockControllerContext(string id, string name, string email, string role)
        {
            var sut = Build();

            SetCurrentUser(id,name,email,role);

            httpResponse.Setup(x => x.SetCookie(It.IsAny<HttpCookie>()));

            sut.ControllerContext = controllerContext.Object;
            return sut;
        }

        internal UserController Build()
        {
            return new UserController(_authService, _userService);
        }
    }
}