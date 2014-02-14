using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

using set.web.Data.Services;
using set.web.Helpers;
using set.web.Models;

namespace set.web.Controllers
{
    public class DomainObjectController : BaseController
    {
        private readonly IDomainObjectService _domainObjectService;

        public DomainObjectController(IDomainObjectService domainObjectService)
        {
            _domainObjectService = domainObjectService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult New()
        {
            return View(new DomainObjectModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> New(DomainObjectModel model)
        {
            SetPleaseTryAgain(model);

            if (model.IsNotValid())
            {
                return View(model);
            }

            model.IsOk = await _domainObjectService.Create(model.Name, User.Identity.GetEmail());
            if (!model.IsOk) return View(model);

            if (!model.IsButtonSaveAndNew) return RedirectToAction("list");

            model.IsOk = true;
            model.Name = string.Empty;
            model.Msg = SetHtmlHelper.LocalizationString("data_saved_successfully_msg");
            return View(model);
        }

        [HttpGet]
        public async Task<ViewResult> List(int id = 1)
        {
            var result = await _domainObjectService.GetDomainObjects(id);
            var list = result.Items.Select(DomainObjectModel.Map).ToList();
            var model = new PageModel<DomainObjectModel>
            {
                Items = list,
                HasNextPage = result.HasNextPage,
                HasPreviousPage = result.HasPreviousPage,
                Number = result.Number,
                TotalCount = result.TotalCount,
                TotalPageCount = result.TotalPageCount
            };
            return View(model);
        }

        [HttpGet]
        public async Task<ViewResult> Detail(string id)
        {
            var result = await _domainObjectService.GetId(id);
            var model = DomainObjectModel.Map(result);
            return View(model);
        }
    }
}