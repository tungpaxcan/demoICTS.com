using ICTS.com.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows;

namespace ICTS.com.Areas.ICTS.Controllers
{
    public class DefaultController : Controller
    {
        // GET: ICTS/Default
        private Entities db = new Entities();
        // GET: Admin/Admin
        public ActionResult Login()
        {
            if (Session["admin"] != null)
            {
                return RedirectToAction("Index", "HomeAdmin");
            }
            return View();
        }
        //[HttpPost]
        //public ActionResult Login(string user, string pass, bool admin)
        //{
        //    var username = user;
        //    var password = pass;
        //    var acc = db.Admins.SingleOrDefault(x => x.Users == username && x.Pass == password && x.Role == admin);
        //    if (acc != null)
        //    {
        //        Session["admin"] = acc;
        //        return RedirectToAction("Index", "HomeAdmin");
        //    }
        //    else
        //    {
        //        return RedirectToAction("No,Thank");
        //    }

        //}
        [HttpPost]
        public JsonResult Login(string user, string pass, bool admin)
        {
            try
            {

                var username = user;
                var password = pass;
                var ad = admin;
                var acc = db.Admins.SingleOrDefault(x => x.Users == username && x.Pass == password && x.Role == ad);
                if (acc != null )
                {
                    Session["admin"] = acc;
                    return Json(new { code = 100, Url = "/ICTS/HomeAdmin/Index/" }, JsonRequestBehavior.AllowGet);
                }
                    return Json(new { code = 300, msg = "Đăng nhập thất bại" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Đăng nhập thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}