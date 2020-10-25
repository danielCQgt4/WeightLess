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
                        filterContext.RequestContext.HttpContext.Session["User"] = UserViewModel.Converter(user);
                    }
                }

            }
        }
    }
}