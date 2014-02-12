using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
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

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet, AllowAnonymous]
        public ActionResult New()
        {
            return View(new DomainObjectModel());
        }

        [HttpPost, ValidateAntiForgeryToken, AllowAnonymous]
        public async Task<ActionResult> New(DomainObjectModel model)
        {
            if (!model.IsValid())
            {
                //todo: locale stringler gelecek
                model.Msg = "Bilgileri eksiksiz doldurunuz.";
                return View(model);
            }

            model.IsOk = await _domainObjectService.Create(model.Name, User.Identity.GetEmail());
            if (!model.IsOk)
            {
                //todo: locale stringler gelecek
                model.Msg = "Kayıt işlemi başarısız.";
            }

            model.Msg = "Kayıt işlemi başarılı.";
            return View(model);
        }

    }
}