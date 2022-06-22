using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ICTS.com.Models;

namespace ICTS.com.Controllers
{
    public class PromotionsController : Controller
    {
        private Entities db = new Entities();
        // GET: Promotions
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult PromotionImg()
        {
            try
            {
                var dmsp = (from s in db.Promotions.Where(x => x.StatusImage != false)
                            select new
                            {
                                id = s.Id,
                                title = s.Title,
                                image = s.Image
                            }).Take(4).ToList();
                return Json(new { code = 200, dmsp = dmsp, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}