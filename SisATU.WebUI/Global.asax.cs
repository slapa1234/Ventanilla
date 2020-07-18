using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SisATU.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        //void Session_Start(object sender, EventArgs e)
        //{
        //    Session.Timeout = 1440;
        //}

        void Session_End(Object sender, EventArgs E) //se ejecuta cuando la sesion se vence
        {
            Session.Clear();
            Session.Abandon();
            // Call your method  
            //Response.RedirectToRoute("Default");
        }
    }
}
