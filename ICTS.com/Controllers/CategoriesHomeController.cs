using ICTS.com.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ICTS.com.Controllers
{
    public class CategoriesHomeController : Controller
    {
        private Entities db = new Entities();
        // GET: Categories
        public ActionResult Index()
        {
            return View();
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
    }
}