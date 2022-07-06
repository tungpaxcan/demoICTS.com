using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ICTS.com.Models;

namespace ICTS.com.Areas.ICTS.Controllers
{
    public class SeosController : Controller
    {
        private Entities db = new Entities();
        // GET: ICTS/Seos
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
                return Json(new { code = 200,dmsp = dmsp, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult Add(string name,string content)
        {
            try
            {
                var a = new SEO();
                a.name = name;
                a.content = content;
                db.SEOs.Add(a);
                db.SaveChanges();
                return Json(new { code = 200, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult Edit(int id,string name, string content)
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                var a = db.SEOs.SingleOrDefault(x => x.Id == id);
                a.name = name;
                a.content = content;
                db.SaveChanges();
                return Json(new { code = 200, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                var a = db.SEOs.SingleOrDefault(x => x.Id == id);
                db.SEOs.Remove(a);
                db.SaveChanges();
                return Json(new { code = 200, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}