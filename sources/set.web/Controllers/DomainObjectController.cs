using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using set.web.Data.Entities;
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

            if (!model.IsValid())
            {
                return View(model);
            }

            model.IsOk = await _domainObjectService.Create(model.Name, User.Identity.GetEmail());
            if (model.IsOk && model.IsButtonSaveAndNew)
                return View(new DomainObjectModel() { IsOk = true, Msg = SetHtmlHelper.LocalizationString("data_saved_successfully_msg") });

            if (!model.IsButtonSaveAndNew)
                return RedirectToAction("List");

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

    }
}