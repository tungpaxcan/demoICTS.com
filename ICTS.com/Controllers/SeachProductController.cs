using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ICTS.com.Models;

namespace ICTS.com.Controllers
{
    public class SeachProductController : Controller
    {
        private Entities db = new Entities();
        // GET: SeachProduct
        public ActionResult Index()
        {
            return View();
        }

    }
}