using System.Threading.Tasks;
using System.Web.Mvc;

using set.web.Data.Services;
using set.web.Models;

namespace set.web.Controllers
{
    public class SearchController : BaseController
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpGet, AllowAnonymous]
        public async Task<JsonResult> Query(string q)
        {
            var model = new ResponseModel { IsOk = false };
            if (string.IsNullOrWhiteSpace(q))
            {
                return Json(model, JsonRequestBehavior.AllowGet);
            }

            var result = await _searchService.Query(q);
            if (result == null)
            {
                return Json(model, JsonRequestBehavior.AllowGet);
            }

            model.Result = result;
            model.IsOk = result.Count > 0;

            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}