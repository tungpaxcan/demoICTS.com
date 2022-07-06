using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ICTS.com.Models;

namespace ICTS.com.Controllers
{
    public class SliderController : Controller
    {
        private Entities db = new Entities();

        // GET: Sliders
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult Slider()
        {
            try
            {
                var slider = (from s in db.Sliders.Where(x => x.Status == true)

                              select new
                              {
                                  id = s.Id,
                                  image = s.Image,
                                  title = s.Title
                              }).ToList();
                var count = slider.Count();
                return Json(new { code = 200, count= count, slider = slider, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
