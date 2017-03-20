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
        private ICinemaWork work;
        public AccountController(ICinemaWork work)
        {
            this.work = work;
        }
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                // поиск пользователя в бд
                User user = null;
                user = work.Users.GetByEmailAndPassword(model.Name, model.Password);
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Name, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                }
            }

            return View(model);
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            UserRepository users = work.Users;
            if (ModelState.IsValid)
            {
                User user = null;
                user = users.GetByEmail(model.Name);
                if (user == null)
                {
                    //create a new one
                    user = new User { Name = model.Name, Password = model.Password, Email = model.Name };
                    users.Create(user);
                    work.Save();
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
            }
            else
            {
                return View(model);
            }
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}