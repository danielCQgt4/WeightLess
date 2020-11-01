using Backend.DAL;
using Backend.Entity;
using Backend.IMPL;
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
            List<Activity> actvs;
            List<ActivityViewModel> activities = new List<ActivityViewModel>();
            using (var u = new UnitWork<Activity>()) {
                actvs = u.genericDAL.GetAll().ToList();
                if (actvs != null) {
                    activities = ActivityViewModel.Converter(actvs);
                }
            }
            foreach (var actv in activities) {
                QRImpl QRimpl = new QRImpl();
                byte[] QRimage = QRimpl.genQR(actv.idActivity + "");
                if (QRimage != null) {
                    actv.qrCode = QRimage;
                }
            }
            return View(activities);
        }
    }

}