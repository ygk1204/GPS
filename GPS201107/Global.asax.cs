using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GPS201107
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Main", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
            routes.MapRoute(
                "Menu", // Route name
                "Menu/Download/{suppliercode}/{category}/{filename}", // URL with parameters
           new { controller = "Menu", action = "Download", suppliercode = UrlParameter.Optional, category = UrlParameter.Optional, filename = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "Board", // Route name
                "Board/Index/{id}/{page}", // URL with parameters
                new { controller = "Board", action = "Index", page = UrlParameter.Optional } // Parameter defaults
            );
            routes.MapRoute(
                "Board_downloadfile", // Route name
                "Board/Download/{boardname}/{listnumber}/{filename}", // URL with parameters
                new { controller = "Board", action = "Download", filename = UrlParameter.Optional } // Parameter defaults
            );
            routes.MapRoute(
                "Default_board", // Route name
                "Board/{action}/{boardname}/{listnumber}", // URL with parameters
                new { controller = "Board", action = "Readboard", listnumber = UrlParameter.Optional } // Parameter defaults
            );
            routes.IgnoreRoute("Filemanager/{*pathInfo}");

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);
        }
    }
}