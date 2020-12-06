using Backend.DAL;
using Backend.Entity;
using Backend.IMPL;
using Backend.Models;
using FrontEnd.Models;
using Microsoft.Reporting.WebForms;
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

        public string getRandom() {
            var ran = new Random();
            string res = "";
            int cr = ran.Next(5, 500);
            for (int i = 0; i < cr; i++) {
                int c = ran.Next(97, 122);
                res += (char)c;
            }
            return res;
        }

        [AuthorizeRole(Role.C)]
        public ActionResult UserAssistance() {
            UserViewModel user = (UserViewModel)Session["User"];
            List<AssistanceViewModel> assistances = new List<AssistanceViewModel>();
            List<Activity> activities;
            using (var u = new UnitWork<Activity>()) {
                activities = u.genericDAL.GetAll().ToList();
            }
            using (var u = new UnitWork<Assistance>()) {
                List<Assistance> asis = u.genericDAL.Find(o => o.idUser == user.idUser).ToList();
                if (asis != null) {
                    assistances = AssistanceViewModel.Converter(asis);
                    foreach (var a in assistances) {
                        using (var un = new UnitWork<Activity_Assitance>()) {
                            a.activitieAssistanceViewModel = ActivitieAssistanceViewModel.Converter(un.genericDAL.Find(o => o.idAssistance == a.idAssistance).ToList());
                        }
                        if (a.activitieAssistanceViewModel != null) {
                            foreach (var aav in a.activitieAssistanceViewModel) {
                                aav.activity = ActivityViewModel.Converter(activities.Find(o => o.idActivity == aav.idActivity));
                            }
                        }
                        a.calculateMetaInformation();
                    }
                }
            }
            assistances.Reverse();
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
                    IAssistanceDAL asis = new AssistanceDALImp();
                    AssistanceControl ac = asis.CalcAssistante(UserViewModel.Converter(usu));
                    usu.assistance = ac.Assistance;
                    caseAction = ac.CaseAction;
                    Session["User"] = usu;
                } else {
                    caseAction = -3;
                }
                if (caseAction == -2) {
                    return RedirectToAction("UserHome", "Home");
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


        [AuthorizeRole(Role.A)]
        public ActionResult ReportAssistance() {
            return View();
        }

        [HttpPost]
        [AuthorizeRole(Role.A)]
        [ValidateAntiForgeryToken]
        public ActionResult ReportAssistance(DateTime date) {
            var reportViewer = new ReportViewer {
                ProcessingMode = ProcessingMode.Local,
                ShowExportControls = true,
                ShowParameterPrompts = true,
                ShowPageNavigationControls = true,
                ShowRefreshButton = true,
                ShowPrintButton = true,
                SizeToReportContent = true,
                AsyncRendering = false,
            };

            string rutaReporte = "~/Reports/AssistanceReport.rdlc";
            string rutaServidor = Server.MapPath(rutaReporte);
            reportViewer.LocalReport.ReportPath = rutaServidor;
            var infoFuenteDatos = reportViewer.LocalReport.GetDataSourceNames();
            reportViewer.LocalReport.DataSources.Clear();

            List<sp_Report_Assistance_Result> datosReporte;
            IAssistanceDAL empDAL = new AssistanceDALImp();
            datosReporte = empDAL.sp_Report_Assistance(date);

            ReportDataSource fuenteDatos = new ReportDataSource();
            fuenteDatos.Name = infoFuenteDatos[0];
            fuenteDatos.Value = datosReporte;
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("AssistanceDataSet", datosReporte));

            reportViewer.LocalReport.Refresh();
            ViewBag.ReportViewer = reportViewer;
            return View();
        }

    }

}
