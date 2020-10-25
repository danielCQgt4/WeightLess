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



            return View(publicationsVM);
        }

        public ActionResult Create() {
            return View();
        }
    }
}