using System.Web;
using System.Web.Mvc;
using System.Security.Principal;

using Moq;
using set.web.Configurations;
using set.web.Controllers;
using set.web.Data.Services;

namespace set.web.test.Shared.Builders
{
    public class UserControllerBuilder
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

            var controllerContext = new Mock<ControllerContext>();
            var httpContext = new Mock<HttpContextBase>();
            var httpRequest = new Mock<HttpRequestBase>();
            var httpResponse = new Mock<HttpResponseBase>();
            var user = new Mock<IPrincipal>();
            var currentUser = new Mock<IIdentity>();

            controllerContext.Setup(x => x.HttpContext).Returns(httpContext.Object);
            httpContext.Setup(x => x.Request).Returns(httpRequest.Object);
            httpContext.Setup(x => x.Response).Returns(httpResponse.Object);
            httpContext.Setup(x => x.User).Returns(user.Object);
            user.Setup(x => x.Identity).Returns(currentUser.Object);
            currentUser.Setup(x => x.IsAuthenticated).Returns(true);
            currentUser.Setup(x => x.Name).Returns(string.Format("{0}|{1}|{2}|{3}", id, name, email, role));

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