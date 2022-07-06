using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ICTS.com.Models;

namespace ICTS.com.Controllers
{
    public class SeoController : Controller
    {
        private Entities db = new Entities();
        // GET: Seo
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult Seo()
        {
            try
            {
                var dmsp = (from s in db.SEOs.Where(x => x.Id > 0)
                            select new
                            {
                                id = s.Id,
                                name = s.name,
                                content = s.content
                            }).ToList();
                return Json(new { code = 200, dmsp = dmsp, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}