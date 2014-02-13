using set.web.Controllers;
using set.web.Data.Services;

namespace set.web.test.Shared.Builders
{
    public class HomeControllerBuilder
    {
        private IFeedbackService _feedbackService; 

        public HomeControllerBuilder()
        {
            _feedbackService = null; 
        } 

        internal HomeControllerBuilder WithFeedbackService(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
            return this;
        }

        internal HomeController Build()
        {
            return new HomeController(_feedbackService);
        } 
    }
}