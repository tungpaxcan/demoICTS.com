using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ICTS.com.Models;

namespace ICTS.com.Controllers
{
    public class CustomerController : Controller
    {
        private Entities db = new Entities();
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult CustomerImg()
        {
            try
            {
                var dmsp = (from s in db.Customers.Where(x => x.StatusImage != false)
                            orderby s.ModifileDate descending
                            orderby s.CreateDate descending
                            select new
                            {
                                id = s.Id,
                                title = s.Title,
                                image = s.Image,
                                link = s.link
                            }).ToList();
                return Json(new { code = 200, dmsp = dmsp, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult CustomerImg2()
        {
            try
            {
                var dmsp = (from s in db.Customers.Where(x => x.StatusImage != false)
                            select new
                            {
                                id = s.Id,
                                title = s.Title,
                                image = s.Image,
                                link = s.link
                            }).Take(6).ToList();
                return Json(new { code = 200, dmsp = dmsp, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}