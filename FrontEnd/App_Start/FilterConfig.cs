﻿using FrontEnd.Filters;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AuthFilter());
        }
    }
}
