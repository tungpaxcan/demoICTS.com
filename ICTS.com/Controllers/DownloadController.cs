using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ICTS.com.Models;
namespace ICTS.com.Controllers
{
    public class DownloadController : Controller
    {
        private Entities db = new Entities();
        // GET: Download
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult Download()
        {
            try
            {
                var left = (from s in db.Downloads.Where(x => x.Status != false)
                            select new
                            {
                                id = s.Id,
                                name =s.Name,
                                download = s.Download1
                            }).ToList();
                return Json(new { code = 200, left = left, Url = "/chi-tiet-san-pham/" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult Count(int id)
        {
            try
            {
                var count = db.Downloads.SingleOrDefault(x => x.Id == id);
                if (count.Count == null)
                {
                    count.Count = 1;
                    db.SaveChanges();
                }
                else
                {
                    count.Count += 1;
                    db.SaveChanges();
                }
                
                
                return Json(new { code = 200,  }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}