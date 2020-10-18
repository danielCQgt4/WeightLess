using FrontEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd.Controllers {

    [Authorize]
    public class AssistanceController : Controller {
        // GET: Assistance
        public ActionResult AdminAssistance() {
            return View();
        }

        public ActionResult UserAssistance() {
            return View();
        }


        public ActionResult CreateAssistance() {
            UserViewModel usu = (UserViewModel)Session["user"];
            return RedirectToAction("TestDash");
        }
    }
}