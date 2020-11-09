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
    public class AssistanceController : Controller {
        // GET: Assistance
        public ActionResult AdminAssistance() {
            return View();
        }

        [AuthorizeRole(Role.C)]
        public ActionResult UserAssistance() {
            UserViewModel user = (UserViewModel)Session["User"];
            List<AssistanceViewModel> assistances = new List<AssistanceViewModel>();
            using (var u = new UnitWork<Assistance>()) {
                List<Assistance> asis = u.genericDAL.Find(o => o.idUser == user.idUser).ToList();
                if (asis != null) {
                    assistances = AssistanceViewModel.Converter(asis);
                }
            }
            return View(assistances);
        }

        private Assistance createNewAssistance(UserViewModel usu) {
            Assistance asis = new Assistance() {
                datetime = DateTime.Now,
                idUser = usu.idUser
            };
            bool res;
            using (var uas = new UnitWork<Assistance>()) {
                uas.genericDAL.Add(asis);
                res = uas.Complete();
            }
            if (res) {
                return asis;
            }
            return null;
        }

        [AuthorizeRole(Role.C)]
        public ActionResult CreateAssistance() {
            if (Request.IsAuthenticated) {
                UserViewModel usu = (UserViewModel)Session["User"];
                int caseAction = -1;
                /*
                -1: to view (well) 
                -2: already with assistance
                -3: Error
                */
                if (usu != null) {
                    string actualDt = DateTime.Now.ToString().Split(' ')[0];
                    using (var u = new UnitWork<Assistance>()) {
                        int idAsis = -1;
                        try {
                            idAsis = u.genericDAL.Find(a => a.idUser == usu.idUser).Max(a => a.idAssistance);
                        } catch (Exception e) {

                        }
                        if (idAsis != -1) {
                            Assistance assistance = u.genericDAL.Get(idAsis);
                            if (assistance != null) {
                                string calcDt = assistance.datetime.ToString().Split(' ')[0];
                                if (calcDt.Equals(actualDt)) {
                                    usu.assistance = assistance;
                                    caseAction = -2;
                                } else {
                                    Assistance asis = createNewAssistance(usu);
                                    if (asis != null) {
                                        usu.assistance = asis;
                                        caseAction = -1;
                                    } else {
                                        caseAction = -3;
                                    }
                                }
                            } else {
                                Assistance asis = createNewAssistance(usu);
                                if (asis != null) {
                                    usu.assistance = asis;
                                    caseAction = -1;
                                } else {
                                    caseAction = -3;
                                }
                            }
                        } else {
                            Assistance asis = createNewAssistance(usu);
                            if (asis != null) {
                                usu.assistance = asis;
                                caseAction = -1;
                            } else {
                                caseAction = -3;
                            }
                        }
                    }
                    Session["User"] = usu;
                } else {
                    caseAction = -3;
                }
                if (caseAction == -2) {
                    return RedirectToAction("TestDash", "Home");
                } else {
                    if (caseAction == -1) {
                        ViewBag.msg = "Se ha creado la asistencia con exito";
                        ViewBag.user = usu;
                        ViewBag.status = true;
                    } else {
                        ViewBag.msg = "No se pudo crear la asistencia";
                        ViewBag.user = usu;
                        ViewBag.status = false;
                    }
                    return View("AssistanceCtr");
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}