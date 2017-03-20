using MvcUi.Infrastructure;
using MvcUi.ViewModels;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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
        //переделать под манагеров
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
                    if (user.ConfirmedEmail)
                    {

                        FormsAuthentication.SetAuthCookie(model.Name, true);
                        return RedirectToAction("Index", "Home");

                    }
                    else
                        ModelState.AddModelError("","Не подтвержден Email");
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
                user = users.GetByEmail(model.Email);
                if (user == null)
                {
                    //create a new one
                    user = new User { Name = model.Email, Password = model.Password, Email = model.Email ,ConfirmedEmail=false};
                    users.Create(user);
                    work.Save();
                    if (user != null) 
                        {
                            MailAddress from = new MailAddress("pauluxxx@mail.ru", "Web Registration");
                            // кому отправляем
                            MailAddress to = new MailAddress(user.Email);
                            // создаем объект сообщения
                            MailMessage m = new MailMessage(from, to);
                            // тема письма
                            m.Subject = "Email confirmation";
                            // текст письма - включаем в него ссылку
                            m.Body = string.Format("Для завершения регистрации перейдите по ссылке:" +
                                            "<a href=\"{0}\" title=\"Подтвердить регистрацию\">{0}</a>",
                                Url.Action("ConfirmEmail", "Account", new { Token = user.ID, Email = user.Email }, Request.Url.Scheme));
                            m.IsBodyHtml = true;
                            // адрес smtp-сервера, с которого мы и будем отправлять письмо
                            SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.mail.ru", 2525);
                        // логин и пароль
                            smtp.EnableSsl = true;
                        smtp.Credentials = new System.Net.NetworkCredential("pauluxxx@mail.ru", "5898044p");
                            smtp.Send(m);
                            return RedirectToAction("Confirm", "Account", new { Email = user.Email });
                            //SmtpClient client = new SmtpClient("smtp.mail.ru", 2525); // Здесь указываем смтп сервер и порт, который мы будем использовать 
                            //client.Credentials = new System.Net.NetworkCredential("pauluxxx@mail.ru", "pass"); // Указываем логин и пароль для авторизации 

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
        public string Confirm(string Email)
        {
            return "На почтовый адрес " + Email + " Вам высланы дальнейшие" +
                    "инструкции по завершению регистрации";
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult ConfirmEmail(string Token, string Email)
        {
           User user = work.Users.Get(int.Parse(Token));
            if (user != null)
            {
                if (user.Email == Email)
                {
                    user.ConfirmedEmail = true;
                    work.Users.Update(user);
                    FormsAuthentication.SetAuthCookie(user.Name, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Confirm", "Account", new { Email = user.Email });
                }
            }
                else
                {
                    return RedirectToAction("Confirm", "Account", new { Email = ""});
                }
            }
           
        }
}