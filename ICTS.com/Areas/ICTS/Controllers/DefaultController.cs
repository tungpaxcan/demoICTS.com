using ICTS.com.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        [HttpPost]
        public ActionResult Login(string user, string pass)
        {
            var username = user;
            var password = pass;
            var acc = db.Admins.SingleOrDefault(x => x.Users == username && x.Pass == password);
            if (acc != null)
            {
                Session["admin"] = acc;
                return RedirectToAction("Index", "HomeAdmin");
            }
            else
            {
                return View();
            }

        }

    }
}