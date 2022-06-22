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
        name: "Service",
        url: "dich-vu/{action}/{id}",
        defaults: new { controller = "Service", action = "Index", id = UrlParameter.Optional }
    );
            routes.MapRoute(
        name: "DetailService",
        url: "dich-vu/chi-tiet-dich-vu/{meta}/{id}",
        defaults: new { controller = "Service", action = "Detail", id = UrlParameter.Optional }
    );
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
              name: "Contact",
              url: "lien-he/{action}/{id}",
              defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional }
          );
            routes.MapRoute(
              name: "DetailProduct",
              url: "chi-tiet-san-pham/{meta}/{id}",
              defaults: new { controller = "ProductHome", action = "Details", id = UrlParameter.Optional }
          );
            routes.MapRoute(
               name: "ProCate",
               url: "san-pham/danh-muc/loai-san-pham/{meta}/{id}",
               defaults: new { controller = "CategoryProductHome", action = "ProCateIndex", id = UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "Cate",
               url: "san-pham/danh-muc/{meta}/{id}",
               defaults: new { controller = "CategoryProductHome", action = "Index", id = UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "Category",
               url: "san-pham/{action}/{id}",
               defaults: new { controller = "CategoriesHome", action = "Index", id = UrlParameter.Optional }
           );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );


        }
    }
}
