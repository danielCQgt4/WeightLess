using Backend.DAL;
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
            return View();
        }

        [HttpPost]
        public ActionResult Index(LogInViewModel loginM) {
            try {
                if (ModelState.IsValid) {

                    //Obtengo el usuario
                    IUserDAL us = new UserDALImp();
                    User user = us.Validate_LogIn(loginM.Correo, loginM.Clave);

                    if (user == null) {
                        //ViewBag.DatosIncorrectos = true;
                        return View(loginM);
                    } else if (!user.active) {
                        //ViewBag.Desactivado = true;
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

                        //Session["Nombre"] = user.Nombre;

                        return RedirectToAction("TestDash");
                    }
                } else {
                    return View(loginM);
                }
            } catch (Exception e) {
                return new HttpNotFoundResult(e.Message);
            }
        }

        //[AuthorizeRole(Role.C, Role.A)]
        public ActionResult TestDash() {
            return View();
        }

        public ActionResult LogOut() {
            Session.RemoveAll(); //Eliminar todos los valores de la sesión
                                 //Session.Abandon(); // Se pueden seguir usando los datos y la sesión termina al final
            FormsAuthentication.SignOut();
            Response.Cache.SetCacheability(HttpCacheability.Private);
            Response.Cache.SetNoServerCaching();
            Request.Cookies.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

    }
}