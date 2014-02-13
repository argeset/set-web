using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using Moq;
using set.web.Controllers;
using set.web.Data.Services;

namespace set.web.test.Shared.Builders
{
    public class FeedbackControllerBuilder
    {
        private IFeedbackService _feedbackService;

        public FeedbackControllerBuilder()
        {
            _feedbackService = null;
        }

        internal FeedbackControllerBuilder WithFeedbackService(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
            return this;
        }

        internal FeedbackController BuildWithMockControllerContext(string id, string name, string email, string role)
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

        internal FeedbackController Build()
        {
            return new FeedbackController(_feedbackService);
        }

    }
}