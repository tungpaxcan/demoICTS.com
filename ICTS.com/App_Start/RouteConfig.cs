using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ICTS.com
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
            name: "DetailSolutionHome",
            url: "giai-phap/chi-tiet-giai-phap/{meta}/{id}",
            defaults: new { controller = "SolutionHome", action = "Details", id = UrlParameter.Optional }
        );
            routes.MapRoute(
               name: "SolutionHome",
               url: "giai-phap/{action}/{id}",
               defaults: new { controller = "SolutionHome", action = "Index", id = UrlParameter.Optional }
           );
           
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            
        }
    }
}
