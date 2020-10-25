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

        public ActionResult UserAssistance() {
            return View();
        }

        private Assistance createNewAssistance(UserViewModel usu) {
            Assistance asis = new Assistance() {
                datetime = DateTime.Now,
                idUser = usu.idUser,
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

        public ActionResult CreateAssistance() {
            UserViewModel usu = (UserViewModel)Session["user"];
            int caseAction = -1;
            /*
            -1: to view (well) 
            -2: already with assistance
            -2: Error
            */
            if (usu != null) {
                string actualDt = DateTime.Now.ToString().Split(' ')[0];
                using (var u = new UnitWork<Assistance>()) {
                    int idAsis = u.genericDAL.Find(a => a.idUser == usu.idUser).Max(a => a.idAssistance);
                    Assistance assistance = u.genericDAL.Get(idAsis);
                    if (assistance != null) {
                        string calcDt = assistance.datetime.ToString().Split(' ')[0];
                        if (calcDt.Equals(actualDt)) {
                            //Todo save on session
                            caseAction = -2;
                        } else {
                            Assistance asis = createNewAssistance(usu);
                            if (asis != null) {
                                //Todo save on session
                                caseAction = -1;
                            } else {
                                caseAction = -3;
                            }
                        }
                    } else {
                        Assistance asis = createNewAssistance(usu);
                        if (asis != null) {
                            //Todo save on session
                            caseAction = -1;
                        } else {
                            caseAction = -3;
                        }
                    }
                }
            }
            if (caseAction == -1) {
                return View();
            } else if (caseAction == -2) {
                return RedirectToAction("TestDash");
            } else {
                return RedirectToAction("ErrorA");
            }
        }
    }
}