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
                    ActivityViewModel activity;
                    using (var u = new UnitWork<Activity>()) {
                        activity = ActivityViewModel.Converter(u.genericDAL.Get(aa.idActivity));
                    }
                    if (activity == null) {
                        ViewBag.status = false;
                        ViewBag.msg = "Error al encontrar la actividad leida";
                    } else {
                        ViewBag.status = true;
                        ViewBag.msg = "Actividad en curso";
                    }
                    aa.activity = activity;
                } else {
                    if (id == null) {
                        ViewBag.status = false;
                        ViewBag.msg = "No hay ninguna actividad activa";
                        return RedirectToAction("Index");
                    }
                    ActivityViewModel activity;
                    using (var u = new UnitWork<Activity>()) {
                        activity = ActivityViewModel.Converter(u.genericDAL.Get(id.GetValueOrDefault(-1)));
                    }
                    if (activity != null) {
                        aa = ActivitieAssistanceViewModel.Converter(dalAct.StartActivity(ActivitieAssistanceViewModel.Converter(new ActivitieAssistanceViewModel(user, activity.idActivity))));
                        ViewBag.status = aa != null;
                        ViewBag.msg = "Actividad nueva generada";
                        if (aa == null) {
                            ViewBag.msg = "No se podo generar la sesion de esta actividad";
                        } else {
                            aa.activity = activity;
                        }
                    } else {
                        ViewBag.status = false;
                        ViewBag.msg = "Error al obtener la actividad leida";
                    }

                }
                return View(aa);
            } else {
                ViewBag.status = false;
                ViewBag.msg = "Debes tener una asistencia activa.";
            }
            return View();
        }

        [AuthorizeRole(Role.C)]
        [HttpPost]
        public ActionResult ChangeTime(ActivitieAssistanceViewModel json) {
            //DEPRECATE

            IActivityDAL dalAct = new ActivityImpl();
            object res = dalAct.UpdateTime(json.timeOcurred, json.idActivityAssistance);
            //int timeB = Convert.ToInt32(json.timeOcurred.Replace(":", ""));
            //bool res = false;
            //bool upd = false;
            //bool finished = false;
            //string timeOcurred = "00:00:00";
            //using (var u = new UnitWork<Activity_Assitance>()) {
            //    Activity_Assitance aa = u.genericDAL.Get(json.idActivityAssistance);
            //    if (aa != null) {
            //        finished = aa.end == null;
            //        if (finished) {
            //            if (aa != null) {
            //                int timeA = Convert.ToInt32(aa.timeOcurred.Replace(":", ""));
            //                if (timeA < timeB) {
            //                    aa.timeOcurred = json.timeOcurred;
            //                    u.genericDAL.Update(aa);
            //                    res = u.Complete();
            //                    if (res) {
            //                        timeOcurred = aa.timeOcurred;
            //                    }
            //                    upd = true;
            //                } else {
            //                    timeOcurred = aa.timeOcurred;
            //                }
            //            }
            //        }
            //    }
            //}

            return Json(res);
        }

        [AuthorizeRole(Role.C)]
        [HttpPost]
        public ActionResult StopSessionActivity(ActivitieAssistanceViewModel json) {
            //DEPRECATE
            UserViewModel user = Session["User"] as UserViewModel;
            IActivityDAL dalAct = new ActivityImpl();
            bool res = dalAct.StopTime(json.idActivityAssistance, user.idUser);
            //bool res = false;
            //using (var u = new UnitWork<Activity_Assitance>()) {
            //    Activity_Assitance aa = u.genericDAL.Get(json.idActivityAssistance);
            //    using (var un = new UnitWork<Activity>()) {
            //        string[] parts = aa.timeOcurred.Split(':');
            //        User usu;
            //        using (var unUsu = new UnitWork<User>()) {
            //            usu = unUsu.genericDAL.Get(user.idUser);
            //        }
            //        Activity act = un.genericDAL.Get(aa.idActivity);
            //        if (act != null) {
            //            //h = Convert.ToInt32(parts[0]) * 60;
            //            //m = Convert.ToInt32(parts[1]) + h;
            //            aa.kcal = act.met * 0.0175m * usu.weight;
            //        }
            //    }
            //    aa.status = false;
            //    aa.end = DateTime.Now;
            //    u.genericDAL.Update(aa);
            //    res = u.Complete();
            //}
            return Json(new { res });
        }
    }

}