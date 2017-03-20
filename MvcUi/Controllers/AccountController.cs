using MvcUi.Infrastructure;
using MvcUi.ViewModels;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TeamProject.DAL;
using TeamProject.DAL.Entities;
using TeamProject.DAL.Repositories;
using TeamProject.DAL.Repositories.Interfaces;

namespace MvcUi.Controllers
{
    [CustomErrorHandler]//куда его лучше положить?
    public class AccountController : Controller
    {
        [Inject]
        IRepository<User> users;
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            users = new UnitOfWork().Users;
            if (ModelState.IsValid)
            {
                User user = null;
                users.GetAll().FirstOrDefault(e => e.Email == model.Name);
                if (user == null)
                {
                    //create a new one
                    users.Create(new User { Name = model.Name });
                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.Name, true);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Не удалось создать нового пользователя");
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                    return View(model);
                }
                //if (user == null)
                //{
                //    //create a new one
                //    users.Create(new User { Name = model.Name });
                //    if (user != null)
                //    {
                //        FormsAuthentication.SetAuthCookie(model.Name, true);
                //        return RedirectToAction("Index", "Home");
                //    }
                //    else
                //    {
                //        return null;
                //    }
                //}
                //else
                //{
                //    ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                //}
            }
            else
            {
                return View(model);
            }
        }
    }
}