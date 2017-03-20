using MvcUi.Infrastructure;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcUi.Controllers
{
    [CustomErrorHandler]//куда его лучше положить?
    public class HomeController : Controller
    {
        [Inject]
        public IWeapon weapon { get; set; }
        public ActionResult Index()

        {
           return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Bazuka()
        {
            return View(weapon);
        }
    }

   
}