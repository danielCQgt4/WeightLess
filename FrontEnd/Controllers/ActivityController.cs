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
            if (user.assistance != null) {
                ActivitieAssistanceViewModel aa = null;
                using (var u = new UnitWork<Activity_Assitance>()) {
                    List<Activity_Assitance> acts = u.genericDAL.Find(o => o.end == null && o.kcal == -5 && o.idAssistance == user.assistance.idAssistance).ToList();
                    if (acts != null && acts.Count() > 0) {
                        aa = ActivitieAssistanceViewModel.Converter(acts.First());
                    }
                }
                if (aa != null) {
                    return RedirectToAction("ActiveActivity", new { id = aa.idActivity });
                }
            }
            List<Activity> actvs;
            List<ActivityViewModel> activities = new List<ActivityViewModel>();
            using (var u = new UnitWork<Activity>()) {
                actvs = u.genericDAL.GetAll().ToList();
                if (actvs != null) {
                    activities = ActivityViewModel.Converter(actvs);
                }
            }
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + "/";
            foreach (var actv in activities) {
                QRImpl QRimpl = new QRImpl();
                byte[] QRimage = QRimpl.genQR(baseUrl + "Activity/ActiveActivity/" + actv.idActivity);
                if (QRimage != null) {
                    actv.qrCode = QRimage;
                }
            }
            return View(activities);
        }

        [AuthorizeRole(Role.C)]
        public ActionResult ActiveActivity(Nullable<int> id) {
            UserViewModel user = Session["User"] as UserViewModel;
            if (user.assistance != null) {
                //Check if user, has an active activitie
                ActivitieAssistanceViewModel aa = null;
                using (var u = new UnitWork<Activity_Assitance>()) {
                    List<Activity_Assitance> acts = u.genericDAL.Find(o => o.end == null && o.kcal == -5 && o.idAssistance == user.assistance.idAssistance).ToList();
                    if (acts != null && acts.Count() > 0) {
                        aa = ActivitieAssistanceViewModel.Converter(acts.First());
                    }
                }
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
                        aa = new ActivitieAssistanceViewModel() {
                            end = null,
                            idActivity = activity.idActivity,
                            kcal = -5,
                            start = DateTime.Now,
                            status = false,
                            timeOcurred = "00:00:00",
                            idAssistance = user.assistance.idAssistance
                        };
                        bool res = false;
                        using (var u = new UnitWork<Activity_Assitance>()) {
                            Activity_Assitance ab = ActivitieAssistanceViewModel.Converter(aa);
                            u.genericDAL.Add(ab);
                            res = u.Complete();
                            if (res) {
                                aa = ActivitieAssistanceViewModel.Converter(ab);
                                aa.activity = activity;
                            }
                        }
                        ViewBag.status = res;
                        ViewBag.msg = "Actividad nueva generada";
                        if (!res) {
                            ViewBag.msg = "No se podo generar la sesion de esta actividad";
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
            int timeB = Convert.ToInt32(json.timeOcurred.Replace(":", ""));
            bool res = false;
            bool upd = false;
            bool finished = false;
            string timeOcurred = "00:00:00";
            using (var u = new UnitWork<Activity_Assitance>()) {
                Activity_Assitance aa = u.genericDAL.Get(json.idActivityAssistance);
                if (aa != null) {
                    finished = aa.end == null;
                    if (finished) {
                        if (aa != null) {
                            int timeA = Convert.ToInt32(aa.timeOcurred.Replace(":", ""));
                            if (timeA < timeB) {
                                aa.timeOcurred = json.timeOcurred;
                                u.genericDAL.Update(aa);
                                res = u.Complete();
                                if (res) {
                                    timeOcurred = aa.timeOcurred;
                                }
                                upd = true;
                            } else {
                                timeOcurred = aa.timeOcurred;
                            }
                        }
                    }
                }
            }

            return Json(new { res, timeOcurred, upd, finished });
        }

        [AuthorizeRole(Role.C)]
        [HttpPost]
        public ActionResult StopSessionActivity(ActivitieAssistanceViewModel json) {
            bool res = false;
            using (var u = new UnitWork<Activity_Assitance>()) {
                Activity_Assitance aa = u.genericDAL.Get(json.idActivityAssistance);
                aa.end = DateTime.Now;
                u.genericDAL.Update(aa);
                res = u.Complete();
            }
            return Json(new { res });
        }
    }

}