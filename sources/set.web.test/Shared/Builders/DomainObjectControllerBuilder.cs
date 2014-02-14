using Moq;

using set.web.Controllers;
using set.web.Data.Services;

namespace set.web.test.Shared.Builders
{
    public class DomainObjectControllerBuilder : BaseBuilder
    {
        private IDomainObjectService _domainObjectService;

        public DomainObjectControllerBuilder()
        {
            _domainObjectService = new Mock<IDomainObjectService>().Object;
        }

        internal DomainObjectControllerBuilder WithSearchService(IDomainObjectService feedbackService)
        {
            _domainObjectService = feedbackService;
            return this;
        }

        internal DomainObjectController Build()
        {
            return new DomainObjectController(_domainObjectService);
        }
    }
}