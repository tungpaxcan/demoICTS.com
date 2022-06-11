using ICTS.com.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;

namespace ICTS.com.Controllers
{
    public class CategoriesHomeController : Controller
    {
        private Entities db = new Entities();
        // GET: Categories
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }
        [HttpGet]
        public JsonResult DanhMucSanPham()
        {
            try
            {
                var dmsp = (from s in db.Categories.Where(x => x.Status != false)
                            orderby s.ModifileDate descending
                            orderby s.CreateDate descending
                            select new
                            {
                                id = s.Id,
                                meta = s.Meta,
                                title = s.Title,
                                name=s.Name,
                                image = s.ImageId
                            }).ToList();
                return Json(new { code = 200, dmsp = dmsp, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult ProductCategory(int id)
        {
            //for(int i=1;i<=id.Length ; i++)
            //{
            //    var j = id[i];
            //}
            try
            {
                var dmsp = (from s in db.ProductCategories.Where(x => x.Status != false && x.IdCategory==id )
                            select new
                            {
                                id = s.Id,
                                meta = s.Meta,
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
        [HttpGet]
        public JsonResult Cate(int id)
        {

            try
            {
                var dmsp = (from s in db.Categories.Where(x => x.Status != false && x.Id == id)
                            select new
                            {
                                id = s.Id,
                                meta = s.Meta,
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