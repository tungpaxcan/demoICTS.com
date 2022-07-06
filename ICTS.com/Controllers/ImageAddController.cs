using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ICTS.com.Models;
namespace ICTS.com.Controllers
{
    public class ImageAddController : Controller
    {
        private Entities db = new Entities();
        // GET: ImageAdd
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult ImageById(int id)
        {
            try
            {
                var dmsp = (from s in db.Images.Where(x => x.Status != false && x.IdProduct == id)
                             select new
                             {
                                 id = s.Id,
                                 image = s.Image1,
                             }).ToList();
                var dmsp1 = (from s in db.Products.Where(x => x.Status != false && x.Id == id)
                            select new
                            {
                                id = s.Id,
                                image = s.Image,
                            }).ToList();
                return Json(new { code = 200, dmsp1 = dmsp1,dmsp=dmsp, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult ChangeImg(int id)
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                var tv = db.Images.SingleOrDefault(x => x.Id == id);
                var tv1 = db.Products.SingleOrDefault(x => x.Id == id);
                return Json(new { code = 200, tv = tv,tv1=tv1, msg = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}