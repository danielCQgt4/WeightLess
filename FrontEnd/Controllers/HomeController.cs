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
            UserViewModel u = (UserViewModel)Session["User"];
            if (u != null) {
                if (u.active) {
                    return RedirectToAction("UserHome");
                }
            }
            QRImpl QRimpl = new QRImpl();
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + "/";
            byte[] QRimage = QRimpl.Get_QR_Asistance(baseUrl + "/Assistance/CreateAssistance");
            if (QRimage != null) {
                ViewBag.QRAsistance = QRimage;
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(LogInViewModel loginM) {
            try {
                QRImpl QRimpl = new QRImpl();
                string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + "/";
                byte[] QRimage = QRimpl.Get_QR_Asistance(baseUrl + "/Assistance/CreateAssistance");
                if (QRimage != null) {
                    ViewBag.QRAsistance = QRimage;
                }
                if (ModelState.IsValid) {

                    //Obtengo el usuario
                    IUserDAL us = new UserDALImp();
                    User user = us.Validate_LogIn(loginM.Correo, loginM.Clave);

                    if (user == null) {
                        ViewBag.wrongCredentials = true;
                        return View(loginM);
                    } else if (!user.active) {
                        ViewBag.inactive = true;
                        return View(loginM);
                    } else {

                        //Obtengo los roles
                        List<string> ListaRoles = new List<string>();
                        ListaRoles.Add(user.rol);
                        var roles = String.Join(",", ListaRoles);

                        //Autetico el usuario y guardo algunos de sus datos y sus roles en la sesión
                        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, user.idUser.ToString(), DateTime.Now, DateTime.Now.AddMinutes(60), loginM.Recordarme, roles, FormsAuthentication.FormsCookiePath);
                        string hash = FormsAuthentication.Encrypt(ticket);
                        HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);

                        if (ticket.IsPersistent) {
                            cookie.Expires = ticket.Expiration;
                        }

                        Response.Cookies.Add(cookie);

                        Session["User"] = UserViewModel.Converter(user);

                        return RedirectToAction("UserHome");
                    }
                } else {
                    return View(loginM);
                }
            } catch (Exception e) {
                return new HttpNotFoundResult(e.Message);
            }
        }

        [AuthorizeRole(Role.C, Role.A, Role.E)]
        public ActionResult UserHome() {
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