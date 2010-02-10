using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ZoosManagementSystem.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "SearchEnvironment",                                               // Route name
                "Administration/Environments/{searchCriteria}",                            // URL with parameters
                new { controller = "Administration", action = "SearchEnvironments" }); // Parameter defaults

            routes.MapRoute(
                "Default",                                               // Route name
                "{controller}/{action}/{param}",                            // URL with parameters
                new { controller = "Home", action = "Index", param = "" }); // Parameter defaults
        }

        protected void Application_Start()
        {
            RegisterRoutes(RouteTable.Routes);
        }
    }
}