using Backend.DAL;
using Backend.Entity;
using FrontEnd.Models;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd.Controllers {

    [Authorize]
    public class UserController : Controller {

        // GET: User
        [AuthorizeRole(Role.A)]
        public ActionResult Index() {
            if (TempData["msg"] != null) {
                ViewBag.msg = TempData["msg"].ToString();
                ViewBag.status = Boolean.Parse(TempData["status"].ToString());
                TempData.Remove("msg");
                TempData.Remove("status");
            }
            List<User> users;
            using (var unit = new UnitWork<User>()) {
                users = unit.genericDAL.GetAll().ToList();
            }
            List<UserViewModel> us = new List<UserViewModel>();
            if (users != null) {
                string key = ConfigurationManager.AppSettings["SecretKey"];
                us = UserViewModel.Converter(users);
                foreach (var u in us) {
                    u.email = Security.Security.DecryptString(key, u.email);
                }
            }
            return View("index", us);
        }

        [AuthorizeRole(Role.A)]
        public ActionResult Create() {
            return View();
        }

        [HttpPost]
        [AuthorizeRole(Role.A)]
        public ActionResult Create(UserViewModel userVM) {
            if (ModelState.IsValid) {
                User user = UserViewModel.Converter(userVM);
                UserDALImp imp = new UserDALImp();
                string msg = imp.ValidationUserCreation(user);
                if (msg.Equals("")) {
                    user = imp.Create(user);
                    if (user != null) {
                        TempData["msg"] = "El usuario fue creado";
                        TempData["status"] = true;
                        return RedirectToAction("Index");
                    } else {
                        ViewBag.msg = "El usuario no pudo ser creado";
                        ViewBag.status = false;
                    }
                } else {
                    ViewBag.msg = msg;
                    ViewBag.status = false;
                }
            } else {
                ViewBag.msg = "Revisa la informacion del usuario";
                ViewBag.status = false;
            }
            return View(userVM);
        }

        [AuthorizeRole(Role.A)]
        public ActionResult Edit(int id) {
            if (TempData["msg"] != null) {
                ViewBag.msg = TempData["msg"].ToString();
                TempData.Remove("msg");
            }
            User us;
            UserDALImp imp = new UserDALImp();
            us = imp.Get_User(id);
            us.password = "temp";
            if (!us.rol.Equals("C")) {
                us.height = 10;
                us.weight = 10;
            }
            return View(UserViewModel.Converter(us));
        }

        [HttpPost]
        [AuthorizeRole(Role.A)]
        public ActionResult Edit(UserViewModel userVM) {
            bool result = false;
            User aux;
            using (var unit = new UnitWork<User>()) {
                aux = unit.genericDAL.Get(userVM.idUser);
            }
            if (aux != null) {
                userVM.password = aux.password;
                if (!userVM.rol.Equals("C")) {
                    userVM.height = 10;
                    userVM.weight = 10;
                }
            }
            if (ModelState.IsValid) {
                UserDALImp imp = new UserDALImp();
                string msg = imp.ValidationUserCreation(UserViewModel.Converter(userVM));
                if (msg.Equals("")) {
                    string key = ConfigurationManager.AppSettings["SecretKey"];
                    userVM.email = Security.Security.EncryptString(key, userVM.email);
                    User user = UserViewModel.Converter(userVM);
                    try {
                        using (var unit = new UnitWork<User>()) {
                            unit.genericDAL.Update(user);
                            result = unit.Complete();
                        }
                    } catch (Exception e) {
                        result = false;
                    }
                } else {
                    ViewBag.msg = msg;
                    ViewBag.status = false;
                }
            } else {
                result = false;
            }

            TempData["status"] = result;
            TempData["msg"] = (!result) ? "El usuario no se pudo crear" : "El usuario ha sido editado";
            if (result) {
                return RedirectToAction("Index");
            }
            return View(userVM);
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditPassword(int idUser, string tempPass, string confirm, string type) {
            bool result = false;
            if (idUser < 1 || tempPass.Equals("") || confirm.Equals("")) {
                TempData["msg"] = "Los campos de contraseña son obligatorios";
            } else if (!tempPass.Equals(confirm)) {
                TempData["msg"] = "Las contraseñas no coinciden";
            } else {
                User aux;
                using (var unit = new UnitWork<User>()) {
                    aux = unit.genericDAL.Get(idUser);
                    if (aux != null) {
                        string key = ConfigurationManager.AppSettings["SecretKey"];
                        aux.password = Security.Security.EncryptString(key, tempPass);
                        unit.genericDAL.Update(aux);
                        result = unit.Complete();
                    }
                }
                TempData["msg"] = (!result) ? "La contraseña no se pudo editar" : "Contraseña editada";
            }
            TempData["status"] = result;
            if (type != null && type.Equals("admin")) {
                if (result) {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Edit", new { id = idUser });
            } else {
                return RedirectToAction("EditProfile");
            }
        }

        [AuthorizeRole(Role.A)]
        public ActionResult Details(int id) {
            User us;
            UserDALImp imp = new UserDALImp();
            us = imp.Get_User(id);
            return View(UserViewModel.Converter(us));
        }

        [AuthorizeRole(Role.A)]
        public ActionResult Delete(int id) {
            return View();
        }

        [AuthorizeRole(Role.A)]
        public ActionResult CtrUser(int idUser, bool active) {
            ViewBag.type = !active;
            ViewBag.done = false;
            User us;
            using (var u = new UnitWork<User>()) {
                us = u.genericDAL.Get(idUser);
            }
            if (us != null) {
                using (var u = new UnitWork<User>()) {
                    us.active = !us.active;
                    u.genericDAL.Update(us);
                    ViewBag.status = u.Complete();
                    if (ViewBag.status) {
                        if (!active) {
                            ViewBag.msg = "El usuario ha sido activado";
                        } else {
                            ViewBag.msg = "El usuario ha sido desactivado";
                        }
                    } else {
                        if (!active) {
                            ViewBag.msg = "El usuario no puso ser activado";
                        } else {
                            ViewBag.msg = "El usuario no pudo ser desactivado";
                        }
                    }
                }
            }
            return Index();
        }

        [Authorize]
        public ActionResult EditProfile() {
            UserViewModel u = (UserViewModel)Session["User"];
            u.password = "temp";
            if (TempData["msg"] != null) {
                ViewBag.msg = TempData["msg"].ToString();
                ViewBag.status = Boolean.Parse(TempData["status"].ToString());
                TempData.Remove("msg");
                TempData.Remove("status");
            }
            return View(u);
        }

        [HttpPost]
        [Authorize]
        public ActionResult EditProfile(UserViewModel userVM) {
            string key = ConfigurationManager.AppSettings["SecretKey"];
            bool result = false;
            User aux;
            using (var unit = new UnitWork<User>()) {
                aux = unit.genericDAL.Get(userVM.idUser);
            }
            if (aux != null) {
                userVM.password = aux.password;
                if (!userVM.rol.Equals("C")) {
                    userVM.height = 10;
                    userVM.weight = 10;
                }
            }
            if (ModelState.IsValid) {
                UserDALImp imp = new UserDALImp();
                string msg = imp.ValidationUserCreation(UserViewModel.Converter(userVM));
                if (msg.Equals("")) {
                    userVM.email = Security.Security.EncryptString(key, userVM.email);
                    User user = UserViewModel.Converter(userVM);
                    try {
                        using (var unit = new UnitWork<User>()) {
                            unit.genericDAL.Update(user);
                            result = unit.Complete();
                        }
                        if (result && userVM.rol.Equals("C") && (aux.height != userVM.height || aux.weight != userVM.weight)) {
                            UserDataHistory udh = new UserDataHistory() {
                                date = DateTime.Now,
                                heigth = user.height,
                                weight = user.weight,
                                idUser = user.idUser
                            };
                            using (var u = new UnitWork<UserDataHistory>()) {
                                u.genericDAL.Add(udh);
                                u.Complete();
                            }
                        }
                        ViewBag.msg = ( !result ) ? "No se pudo actualizar el perfil" : "El perfil se ha modificado";
                    } catch (Exception e) {
                        result = false;
                    }
                } else {
                    ViewBag.msg = msg;
                    ViewBag.status = false;
                }
            } else {
                result = false;
            }

            ViewBag.status = result;
            if (result) {
                userVM.password = null;
                userVM.email = Security.Security.DecryptString(key, userVM.email);
                Session["User"] = userVM;
            }
            return EditProfile();
        }

        [AuthorizeRole(Role.C)]
        public ActionResult ReportUserDataHistory() {

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

            string rutaReporte = "~/Reports/UserDataHistoryReport.rdlc";
            string rutaServidor = Server.MapPath(rutaReporte);
            reportViewer.LocalReport.ReportPath = rutaServidor;
            var infoFuenteDatos = reportViewer.LocalReport.GetDataSourceNames();
            reportViewer.LocalReport.DataSources.Clear();

            UserViewModel u = (UserViewModel)Session["User"];
            List<UserDataHistory> datosReporte;
            using (var udh = new UnitWork<UserDataHistory>()) {
                datosReporte = udh.genericDAL.Find(o => o.idUser == u.idUser).ToList();
            }

            ReportDataSource fuenteDatos = new ReportDataSource();
            fuenteDatos.Name = infoFuenteDatos[0];
            fuenteDatos.Value = datosReporte;
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("UserDataHistoryDataSet", datosReporte));

            reportViewer.LocalReport.Refresh();
            ViewBag.ReportViewer = reportViewer;
            return View();
        }


    }
}