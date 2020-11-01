using Backend.DAL;
using Backend.Entity;
using FrontEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd.Controllers {

    [Authorize]
    public class ActivityController : Controller {

        [AuthorizeRole(Role.C)]
        public ActionResult Index() {
            List<Activity> activities;
            using (var u = new UnitWork<Activity>()) {
                activities = u.genericDAL.GetAll().ToList();
            }
            return View(activities);
        }
    }

}