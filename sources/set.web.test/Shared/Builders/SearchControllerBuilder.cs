using Moq;

using set.web.Controllers;
using set.web.Data.Services;

namespace set.web.test.Shared.Builders
{
    public class SearchControllerBuilder : BaseBuilder
    {
        private ISearchService _searchService;

        public SearchControllerBuilder()
        {
            _searchService = new Mock<ISearchService>().Object;
        }

        internal SearchControllerBuilder WithSearchService(ISearchService feedbackService)
        {
            _searchService = feedbackService;
            return this;
        }

        internal SearchController Build()
        {
            return new SearchController(_searchService);
        }
    }
}