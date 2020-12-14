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
            if (TempData["status"] != null) {
                ViewBag.status = TempData["status"];
                ViewBag.msg = TempData["msg"];
                TempData.Clear();
            }
            UserViewModel user = Session["User"] as UserViewModel;
            IActivityDAL dalAct = new ActivityImpl();
            if (user.assistance != null) {
                ActivitieAssistanceViewModel aa = ActivitieAssistanceViewModel.Converter(dalAct.GetCurrentActivity(user.assistance.idAssistance));
                if (aa != null) {
                    return RedirectToAction("ActiveActivity", new { id = aa.idActivity });
                }
            }
            List<Activity> actvs = dalAct.GetActivities();
            List<ActivityViewModel> activities = ActivityViewModel.Converter(actvs);
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + "/";
            foreach (var actv in activities) {
                actv.qrCode = dalAct.PlaceQRInActivity(baseUrl, actv.idActivity);
            }
            return View(activities);
        }

        [AuthorizeRole(Role.C)]
        public ActionResult ActiveActivity(Nullable<int> id) {
            UserViewModel user = Session["User"] as UserViewModel;
            if (user.assistance != null) {
                IActivityDAL dalAct = new ActivityImpl();
                ActivitieAssistanceViewModel aa = ActivitieAssistanceViewModel.Converter(dalAct.GetCurrentActivity(user.assistance.idAssistance));
                if (aa != null) {
                    return RedirectToAction("Working", new { id = aa.idActivityAssistance });
                } else {
                    if (id == null) {
                        TempData["status"] = false;
                        TempData["msg"] = "No hay ninguna actividad activa";
                        return RedirectToAction("Index");
                    }
                    ActivityViewModel activity;
                    using (var u = new UnitWork<Activity>()) {
                        activity = ActivityViewModel.Converter(u.genericDAL.Get(id.GetValueOrDefault(-1)));
                    }
                    if (activity != null) {
                        aa = ActivitieAssistanceViewModel.Converter(dalAct.StartActivity(ActivitieAssistanceViewModel.Converter(new ActivitieAssistanceViewModel(user, activity.idActivity))));
                        TempData["status"] = aa != null;
                        TempData["msg"] = "Actividad nueva generada";
                        if (aa == null) {
                            TempData["msg"] = "No se podo generar la sesion de esta actividad";
                        } else {
                            aa.activity = activity;
                        }
                        return RedirectToAction("Working", new { id = aa.idActivityAssistance });
                    } else {
                        TempData["status"] = false;
                        TempData["msg"] = "Error al obtener la actividad";
                    }
                }
            } else {
                TempData["status"] = false;
                TempData["msg"] = "Debes tener una asistencia activa.";
            }
            return RedirectToAction("Index");
        }

        [AuthorizeRole(Role.C)]
        public ActionResult Working(Nullable<int> id) {
            if (id == null) {
                return RedirectToAction("Index");
            }
            if (TempData["status"] != null) {
                ViewBag.status = TempData["status"];
                ViewBag.msg = TempData["msg"];
            }
            UserViewModel user = Session["User"] as UserViewModel;
            if (user.assistance != null) {
                IActivityDAL dalAct = new ActivityImpl();
                ActivitieAssistanceViewModel aa2 = ActivitieAssistanceViewModel.Converter(dalAct.GetCurrentActivity(user.assistance.idAssistance));
                ActivitieAssistanceViewModel aa = ActivitieAssistanceViewModel.Converter(dalAct.GetActivity_Assistance(id.GetValueOrDefault()));
                if (aa2 == null || aa2.idActivityAssistance != id) {
                    return RedirectToAction("Index");
                }
                ActivityViewModel activity;
                using (var u = new UnitWork<Activity>()) {
                    activity = ActivityViewModel.Converter(u.genericDAL.Get(aa.idActivity));
                }
                if (activity == null) {
                    ViewBag.status = false;
                    ViewBag.msg = "Error al encontrar la actividad actual";
                } else {
                    ViewBag.status = true;
                    ViewBag.msg = "Actividad en curso";
                }
                aa.activity = activity;
                return View(aa);
            } else {
                TempData["status"] = false;
                TempData["msg"] = "Error al obtener la actividad";
            }
            return RedirectToAction("Index");
        }

    }

}