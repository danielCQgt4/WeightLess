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
    public class PublicationController : Controller {


        // GET: Publication
        [AuthorizeRole(Role.C)]
        public ActionResult Index() {
            List<Publication> publications;

            using (var unidad = new UnitWork<Publication>()) {
                publications = unidad.genericDAL.GetAll().ToList();
            }

            List<PublicationViewModel> publicationsVM = new List<PublicationViewModel>();

            publicationsVM = PublicationViewModel.Converter(publications);

            List<User> usuarios;
            using (var unidad = new UnitWork<User>()) {
                usuarios = unidad.genericDAL.Find(u => u.rol == "A").ToList();
            }

            List<Publication_Activity> publicationActivities;
            using (var unidad = new UnitWork<Publication_Activity>()) {
                publicationActivities = unidad.genericDAL.GetAll().ToList();
            }

            List<Activity> activities;
            using (var unidad = new UnitWork<Activity>()) {
                activities = unidad.genericDAL.GetAll().ToList();
            }

            List<Publication_Activity> auxPublicationActivities;

            foreach (var item in publicationsVM) {
                item.User = usuarios.Find(u => u.idUser == item.idUser);

                if (item.type == "A") {
                    List<ActivityViewModel> auxActivities = new List<ActivityViewModel>();
                    auxPublicationActivities = publicationActivities.Where(pa => pa.idPublication == item.idPublication).ToList();
                    foreach (var act in auxPublicationActivities) {
                        var activity = activities.Find(a => a.idActivity == act.idActivity);
                        if (activity != null) {
                            ActivityViewModel acVM = new ActivityViewModel();
                            acVM = ActivityViewModel.Converter(activity);
                            acVM.description = act.description;
                            auxActivities.Add(acVM);
                        }
                    }
                    item.publicationActivities = auxActivities;
                }
            }

            return View(publicationsVM);
        }

        [AuthorizeRole(Role.E)]
        public ActionResult TrainerPublications() {

            UserViewModel user = (UserViewModel)Session["User"];

            List<Publication> publications;
            using (var unidad = new UnitWork<Publication>()) {
                publications = unidad.genericDAL.Find(p => p.idUser == user.idUser).ToList();
            }

            List<PublicationViewModel> publicationsVM = new List<PublicationViewModel>();
            publicationsVM = PublicationViewModel.Converter(publications);

            List<Publication_Activity> publicationActivities;
            using (var unidad = new UnitWork<Publication_Activity>()) {
                publicationActivities = unidad.genericDAL.GetAll().ToList();
            }

            List<Activity> activities;
            using (var unidad = new UnitWork<Activity>()) {
                activities = unidad.genericDAL.GetAll().ToList();
            }

            List<Publication_Activity> auxPublicationActivities;
            foreach (var item in publicationsVM) {
                if (item.type == "A") {
                    List<ActivityViewModel> auxActivities = new List<ActivityViewModel>();
                    auxPublicationActivities = publicationActivities.Where(pa => pa.idPublication == item.idPublication).ToList();
                    foreach (var act in auxPublicationActivities) {
                        var activity = activities.Find(a => a.idActivity == act.idActivity);
                        if (activity != null) {
                            ActivityViewModel acVM = new ActivityViewModel();
                            acVM = ActivityViewModel.Converter(activity);
                            acVM.description = act.description;
                            auxActivities.Add(acVM);
                        }
                    }
                    item.publicationActivities = auxActivities;
                }
            }

            if (publicationsVM.Count == 0) {
                ViewBag.empty = true;
            }

            return View(publicationsVM);
        }

        [AuthorizeRole(Role.E)]
        public ActionResult Create() {
            PublicationViewModel publication = new PublicationViewModel();
            IEnumerable<Activity> activities;
            using (var unidad = new UnitWork<Activity>()) {
                activities = unidad.genericDAL.GetAll().ToList();
                ViewBag.activities = activities;
            }
            return View(publication);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeRole(Role.E)]
        public ActionResult Create(PublicationViewModel publicationVM) {
            try {
                if (ModelState.IsValid) {

                    Publication publication = PublicationViewModel.Converter(publicationVM);
                    publication.datetime = DateTime.Now;
                    publication.idUser = Convert.ToInt32(HttpContext.User.Identity.Name);
                    publication.likes = 0;
                    publication.disLikes = 0;

                    bool result = true;

                    if (publicationVM.type == "N") {
                        using (var unitP = new UnitWork<Publication>()) {
                            unitP.genericDAL.Add(publication);
                            if (unitP.Complete()) {
                                TempData["pCreated"] = "El consejo ha sido creado"; //TODO poner mensaje en la vista
                            } else {
                                TempData["errorCreate"] = "No se ha podido crear el consejo"; //TODO poner mensaje en la vista
                                result = false;
                            }
                        }
                    } else {
                        if (publicationVM.activities == null) {
                            TempData["errorCreate"] = "El tipo de consejo requiere de almenos una actividad";
                            result = false;
                        } else {

                            using (var unitP = new UnitWork<Publication>()) {
                                unitP.genericDAL.Add(publication);
                                if (!unitP.Complete()) {
                                    TempData["errorCreate"] = "No se ha podido crear el consejo"; //TODO poner mensaje en la vista
                                    result = false;
                                }
                            }

                            if (result) {
                                List<Publication_Activity> tempActivities = new List<Publication_Activity>();
                                //var cont = 1;
                                foreach (var item in publicationVM.activities) {
                                    string[] auxAct = item.Split(':');
                                    if (auxAct[0] == "" || auxAct[1] == "") {
                                        //TempData["errorCreate"] = "Debe completar la actividad #" + cont;
                                        using (var unitP = new UnitWork<Publication>()) {
                                            unitP.genericDAL.Remove(publication);
                                            unitP.Complete();
                                        }
                                        TempData["errorCreate"] = "Alguna actividad no está completa";
                                        result = false;
                                        break;
                                    } else {
                                        Publication_Activity auxPA = new Publication_Activity();
                                        auxPA.idPublication = publication.idPublication;
                                        auxPA.idActivity = Convert.ToInt32(auxAct[0]);
                                        auxPA.description = auxAct[1];
                                        tempActivities.Add(auxPA);
                                    }
                                    //cont++;
                                }

                                if (result) {
                                    using (var unitPA = new UnitWork<Publication_Activity>()) {
                                        unitPA.genericDAL.AddRange(tempActivities);
                                        if (unitPA.Complete()) {
                                            TempData["pCreated"] = "El consejo ha sido creado"; //TODO poner mensaje en la vista
                                        } else {
                                            using (var unitP = new UnitWork<Publication>()) {
                                                unitP.genericDAL.Remove(publication);
                                                unitP.Complete();
                                            }
                                            TempData["errorCreate"] = "No se ha podido crear el consejo"; //TODO poner mensaje en la vista
                                            result = false;
                                        }

                                    }
                                }
                            }
                        }
                    }

                    if (result) {
                        return RedirectToAction("TrainerPublications");
                    } else {
                        IEnumerable<Activity> activities;
                        using (var unidad = new UnitWork<Activity>()) {
                            activities = unidad.genericDAL.GetAll().ToList();
                            ViewBag.activities = activities;
                        }
                        return View(publicationVM);
                    }

                } else {
                    return View(publicationVM);
                }
            } catch (Exception e) {
                return new HttpNotFoundResult(e.Message);
            }
        }

        [HttpPost]
        [AuthorizeRole(Role.E)]
        public ActionResult Delete(int id) {
            Publication publication;
            IEnumerable<Publication_Activity> publicationsActivity;
            bool res = false;
            using (DBContext context = new DBContext()) {
                using (var tran = context.Database.BeginTransaction()) {
                    using (var unitPA = new UnitWork<Publication_Activity>(context)) {
                        using (var unitP = new UnitWork<Publication>()) {
                            publication = unitP.genericDAL.Get(id);
                        }

                        if (publication.type == "A") {
                            publicationsActivity = unitPA.genericDAL.Find(pa => pa.idPublication == id);
                            unitPA.genericDAL.RemoveRange(publicationsActivity);
                            res = unitPA.Complete();
                        }

                        if (res || publication.type == "N") {
                            using (var unitP = new UnitWork<Publication>(context)) {
                                unitP.genericDAL.Remove(publication);
                                res = unitP.Complete();

                                if (res) {
                                    TempData["successDelete"] = "El consejo ha sido eliminado";
                                } else {
                                    TempData["errorDelete"] = "No se ha podido eliminar el consejo";
                                    tran.Rollback();
                                }
                                tran.Commit();
                            }
                        } else {
                            TempData["errorDelete"] = "No se ha podido eliminar el consejo";
                            tran.Rollback();
                            tran.Commit();
                        }
                    }

                }
            }
            return Json(new {});
            //return RedirectToAction("TrainerPublications");
        }


    }
}