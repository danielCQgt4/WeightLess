using Backend.DAL;
using Backend.Entity;
using FrontEnd.Models;
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

        public ActionResult Create() {
            return View();
        }

        [HttpPost]
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

        public ActionResult Edit(int id) {
            User us;
            UserDALImp imp = new UserDALImp();
            us = imp.Get_User(id);
            us.password = "";
            return View(UserViewModel.Converter(us));
        }

        [HttpPost]
        public ActionResult Edit(UserViewModel userVM) {
            if (ModelState.IsValid) {
                User user = UserViewModel.Converter(userVM);
                try {
                    using (var unit = new UnitWork<User>()) {
                        unit.genericDAL.Update(user);
                        ViewBag.create = unit.Complete();
                    }
                } catch (Exception e) {
                    ViewBag.create = false;
                }
            } else {
                ViewBag.create = false;
            }
            return View(userVM);//Temp
        }

        public ActionResult Details(int id) {
            User us;
            UserDALImp imp = new UserDALImp();
            us = imp.Get_User(id);
            return View(UserViewModel.Converter(us));
        }

        public ActionResult Delete(int id) {
            return View();
        }

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
                    ViewBag.done = u.Complete();
                }
            }
            return Index();
        }

    }

}