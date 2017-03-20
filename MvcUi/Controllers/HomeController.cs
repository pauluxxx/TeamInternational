using MvcUi.Infrastructure;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeamProject.DAL.Entities;
using TeamProject.DAL.Repositories.Interfaces;

namespace MvcUi.Controllers
{
    [CustomErrorHandler]//куда его лучше положить?
    public class HomeController : Controller
    {
        
        public string Index()
        {
            string result = "Вы не авторизованы";
            if (User.Identity.IsAuthenticated)
            {
                result = "Ваш логин: " + User.Identity.Name;
            }
            return result;
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

    }

   
}