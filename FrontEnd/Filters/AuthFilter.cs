using Backend.DAL;
using Backend.Entity;
using FrontEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace FrontEnd.Filters {
    public class AuthFilter : FilterAttribute, IAuthorizationFilter {

        public void OnException(ExceptionContext filterContext) {
            throw new NotImplementedException();
        }

        public void OnAuthorization(AuthorizationContext filterContext) {
            if (filterContext.RequestContext.HttpContext.Request.IsAuthenticated) {

                IUserDAL us = new UserDALImp();
                User user = us.Get_User(Convert.ToInt32(filterContext.RequestContext.HttpContext.User.Identity.Name));
                if (user != null) {
                    if (!user.active) {
                        //ViewBag.Desactivado = true;
                        filterContext.RequestContext.HttpContext.RedirectLocal("/Homa/Index");
                    } else {
                        UserViewModel userV = UserViewModel.Converter(user);
                        Assistance a = loadAssistance(user.idUser);
                        if (a != null) {
                            userV.assistance = a;
                        }
                        filterContext.RequestContext.HttpContext.Session["User"] = userV;
                    }
                }

            }
        }

        private Assistance loadAssistance(int idUser) {
            string actualDt = DateTime.Now.ToString().Split(' ')[0];
            Assistance assistance = null;
            using (var u = new UnitWork<Assistance>()) {
                int idAsis = -1;
                try {
                    idAsis = u.genericDAL.Find(a => a.idUser == idUser).Max(a => a.idAssistance);
                    if (idAsis != -1) {
                        assistance = u.genericDAL.Get(idAsis);
                        string calcDt = assistance.datetime.ToString().Split(' ')[0];
                        if (calcDt.Equals(actualDt)) {
                            assistance = u.genericDAL.Get(idAsis);
                        }
                    }
                } catch (Exception e) {

                }
            }
            return assistance;
        }
    }
}