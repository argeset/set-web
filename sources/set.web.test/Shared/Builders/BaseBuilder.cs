using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

using Moq;

namespace set.web.test.Shared.Builders
{
    public class BaseBuilder
    {
        public Mock<ControllerContext> controllerContext = new Mock<ControllerContext>();
        public Mock<HttpContextBase> httpContext = new Mock<HttpContextBase>();
        public Mock<HttpRequestBase> httpRequest = new Mock<HttpRequestBase>();
        public Mock<HttpResponseBase> httpResponse = new Mock<HttpResponseBase>();
        public Mock<IPrincipal> user = new Mock<IPrincipal>();
        public Mock<IIdentity> currentUser = new Mock<IIdentity>();

        public void SetCurrentUser(string id, string name, string email, string role)
        {
            controllerContext.Setup(x => x.HttpContext).Returns(httpContext.Object);
            httpContext.Setup(x => x.Request).Returns(httpRequest.Object);
            httpContext.Setup(x => x.Response).Returns(httpResponse.Object);
            httpContext.Setup(x => x.User).Returns(user.Object);
            user.Setup(x => x.Identity).Returns(currentUser.Object);
            currentUser.Setup(x => x.IsAuthenticated).Returns(true);
            currentUser.Setup(x => x.Name).Returns(string.Format("{0}|{1}|{2}|{3}", id, name, email, role));

            httpResponse.Setup(x => x.SetCookie(It.IsAny<HttpCookie>()));
        }  
    }
}