using FrontEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FrontEnd.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LogInViewModel loginM) {
            if (ModelState.IsValid) {
                return RedirectToAction("TestDash");
            } else {
                return RedirectToAction("Index");
            }
        }

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