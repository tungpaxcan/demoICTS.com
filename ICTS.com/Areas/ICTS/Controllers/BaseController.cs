using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ICTS.com.Areas.ICTS.Controllers
{
    public class BaseController : Controller
    {
        // GET: ICTS/Base
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["admin"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary(new { controller = "Default", action = "Login" }));
            }
            base.OnActionExecuting(filterContext);
        }
    }
}