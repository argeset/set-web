using Moq;

using set.web.Controllers;
using set.web.Data.Services;

namespace set.web.test.Shared.Builders
{
    public class HomeControllerBuilder : BaseBuilder
    {
        private IFeedbackService _feedbackService;
        private IAuthService _authService;
        private IUserService _userService;

        public HomeControllerBuilder()
        {
            _feedbackService = new Mock<IFeedbackService>().Object;
            _authService = new Mock<IAuthService>().Object;
            _userService = new Mock<IUserService>().Object;
        }

        internal HomeControllerBuilder WithFeedbackService(IFeedbackService service)
        {
            _feedbackService = service;
            return this;
        }
        internal HomeControllerBuilder WithAuthService(IAuthService service)
        {
            _authService = service;
            return this;
        }
        internal HomeControllerBuilder WithUserService(IUserService service)
        {
            _userService = service;
            return this;
        }

        internal HomeController Build()
        {
            return new HomeController(_authService, _userService, _feedbackService);

        }
    }
}