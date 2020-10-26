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
                    List<Activity> auxActivities = new List<Activity>();
                    auxPublicationActivities = publicationActivities.Where(pa => pa.idPublication == item.idPublication).ToList();
                    foreach (var act in auxPublicationActivities) {
                        var activity = activities.Find(a => a.idActivity == act.idActivity);
                        if (activity != null) {
                            auxActivities.Add(activity);
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
                    List<Activity> auxActivities = new List<Activity>();
                    auxPublicationActivities = publicationActivities.Where(pa => pa.idPublication == item.idPublication).ToList();
                    foreach (var act in auxPublicationActivities) {
                        var activity = activities.Find(a => a.idActivity == act.idActivity);
                        if (activity != null) {
                            auxActivities.Add(activity);
                        }
                    }
                    item.publicationActivities = auxActivities;
                }
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
                    using (var unit = new UnitWork<Publication>()) {
                        publication.datetime = DateTime.Now;
                        publication.idUser = Convert.ToInt32(HttpContext.User.Identity.Name);
                        publication.likes = 0;
                        publication.disLikes = 0;
                        unit.genericDAL.Add(publication);
                        if (unit.Complete()) {
                            TempData["publicationCreated"] = false; //TODO poner mensaje en la vista
                            return RedirectToAction("TrainerPublications");
                        } else {
                            ViewBag.errorCreate = true; //TODO poner mensaje en la vista
                            return View(publicationVM);
                        }
                    }
                } else {
                    return View(publicationVM);
                }
            } catch (Exception e) {
                return new HttpNotFoundResult(e.Message);
            }
        }
    }
}