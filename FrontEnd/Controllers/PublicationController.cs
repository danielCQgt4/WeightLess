using Backend.DAL;
using Backend.Entity;
using FrontEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd.Controllers {
    public class PublicationController : Controller {
        // GET: Publication
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

        public ActionResult Create() {
            return View();
        }
    }
}