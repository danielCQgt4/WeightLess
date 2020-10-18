using Backend.DAL;
using Backend.IMPL;
using Backend.Entity;
using FrontEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FrontEnd.Controllers {
    public class HomeController : Controller {

        public ActionResult Index() {
            if (Request.IsAuthenticated) {

                IUserDAL us = new UserDALImp();
                User user = us.Get_User(Convert.ToInt32(HttpContext.User.Identity.Name));
                if (user != null) {
                    if (!user.active) {
                        //ViewBag.Desactivado = true;
                        return View();
                    } else {
                        Session["User"] = UserViewModel.Converter(user);
                        return RedirectToAction("TestDash");
                    }
                }

            }
            QRImpl QRimpl = new QRImpl();
            byte[] QRimage = QRimpl.Get_QR_Asistance();
            if (QRimage != null) {
                ViewBag.QRAsistance = QRimage;
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(LogInViewModel loginM) {
            try {
                if (ModelState.IsValid) {


                    //Obtengo el usuario
                    IUserDAL us = new UserDALImp();
                    User user = us.Validate_LogIn(loginM.Correo, loginM.Clave);

                    if (user == null)
                    {
                        ViewBag.WrongCredentials = true;
                        return View(loginM);
                    }
                    else if (!user.active)
                    {
                        ViewBag.Inactive = true;
                        return View(loginM);
                    }
                    else
                    {

                        //Obtengo los roles
                        List<string> ListaRoles = new List<string>();
                        ListaRoles.Add(user.rol);
                        var roles = String.Join(",", ListaRoles);

                        //Autetico el usuario y guardo algunos de sus datos y sus roles en la sesión
                        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, user.idUser.ToString(), DateTime.Now, DateTime.Now.AddMinutes(60), loginM.Recordarme, roles, FormsAuthentication.FormsCookiePath);
                        string hash = FormsAuthentication.Encrypt(ticket);
                        HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);

                        if (ticket.IsPersistent)
                        {
                            cookie.Expires = ticket.Expiration;
                        }
                        Response.Cookies.Add(cookie);

                        Session["User"] = UserViewModel.Converter(user);

                        return RedirectToAction("TestDash");
                    }
                } else {
                    return View(loginM);
                }
            } catch (Exception e) {
                return new HttpNotFoundResult(e.Message);
            }
        }

        [AuthorizeRole(Role.C, Role.A, Role.E)]
        public ActionResult TestDash() {
            return View();
        }

        public ActionResult LogOut() {
            Session.RemoveAll();
            FormsAuthentication.SignOut();
            Response.Cache.SetCacheability(HttpCacheability.Private);
            Response.Cache.SetNoServerCaching();
            Request.Cookies.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

    }
}