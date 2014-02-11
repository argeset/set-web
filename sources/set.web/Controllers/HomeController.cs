﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using set.web.Data.Services;
using set.web.Helpers;
using set.web.Models;

namespace set.web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IFeedbackService _feedbackService;

        public HomeController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet, AllowAnonymous]
        public ViewResult Contact()
        {
            var model = new ContactModel();

            if (User.Identity.IsAuthenticated)
            {
                model.Email = User.Identity.GetEmail();
            }

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, AllowAnonymous]
        public ActionResult Contact(ContactModel model)
        {
            if (!model.IsValid())
            {
                SetPleaseTryAgain(model);
                return View(model);
            }

            //todo send via service
            model.IsOk = true;

            if (model.IsOk)
            {
                model.Msg = SetHtmlHelper.LocalizationString("data_saved_successfully_msg");
            }

            return View(model);
        }
    }
}