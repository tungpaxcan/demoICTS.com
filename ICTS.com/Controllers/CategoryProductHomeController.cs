using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ICTS.com.Models;

namespace ICTS.com.Controllers
{
    [HandleError]
    public class CategoryProductHomeController : Controller
    {
        private Entities db = new Entities();
        // GET: CategoryProductHome
        public ActionResult Index(string meta,int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category cate = db.Categories.Find(id);
            if (cate == null)
            {
                return HttpNotFound();
            }
            return View(cate);
        }
        public ActionResult ProCateIndex(string meta, int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCategory procate = db.ProductCategories.Find(id);
            if (procate == null)
            {
                return HttpNotFound();
            }
            return View(procate);
        }
        [HttpGet]
        public JsonResult Cate(int id)
        {
            try
            {

                var dmsp = (from s in db.Categories.Where(x => x.Id == id)
                            select new
                            {
                                id = s.Id,
                                meta = s.Meta,
                                title = s.Title,
                                name = s.Name
                            }).ToList();
                return Json(new { code = 200, dmsp = dmsp, Url = "/san-pham/danh-muc/", msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult ProCateUrl(int id)
        {
            try
            {

                var dmsp = (from s in db.ProductCategories.Where(x => x.Id == id)
                            select new
                            {
                                id = s.Id,
                                meta = s.Meta,
                                title = s.Title,
                                name = s.Name
                            }).ToList();
                return Json(new { code = 200, dmsp = dmsp, Url = "/san-pham/danh-muc/loai-san-pham/", msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult ProCate(int id)
        {
            try
            {

                var dmsp = (from s in db.ProductCategories.Where(x => x.IdCategory == id)
                            select new
                            {
                                id = s.Id,
                                meta = s.Meta.Replace("&", ""),
                                title = s.Title,
                                name = s.Name
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