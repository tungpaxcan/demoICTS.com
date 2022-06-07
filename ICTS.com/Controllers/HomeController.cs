using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ICTS.com.Models;

namespace ICTS.com.Controllers
{
    public class HomeController : Controller
    {
        private Entities db = new Entities();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult Logo()
        {
            try
            {
                var logo = (from tt in db.Modules.Where(x => x.Id == 5)
                            select new
                            {
                                id = tt.Id,                              
                                title=tt.Title,
                                image=tt.Image
                            }).ToList();
                return Json(new { code = 200, logo = logo, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult TTLHKD()
        {
            try
            {
                var ttlh = (from tt in db.Modules.Where(x => x.Id==1||x.Id==2)
                            select new
                            {
                                id=tt.Id,
                                title=tt.Title,
                                name = tt.Name,
                                phone = tt.Phone
                            }).ToList();
                return Json(new { code = 200, ttlh = ttlh, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult CSKH()
        {
            try
            {
                var ttlh = (from tt in db.Modules.Where(x => x.Id == 3 || x.Id==4)
                            select new
                            {
                                id=tt.Id,
                                title = tt.Title,
                                name = tt.Name,
                                phone = tt.Phone
                            }).ToList();
                return Json(new { code = 200, ttlh = ttlh, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult DanhMucTheoAnh()
        {
            try
            {
                var dmsp = (from s in db.Categories.Where(x => x.StatusImage != false)
                            select new
                            {
                                id = s.Id,
                                title = s.Title,
                                image = s.ImageId
                            }).Take(4).ToList();
                return Json(new { code = 200, dmsp = dmsp, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult Why()
        {
            try
            {
                var w = (from s in db.Modules.Where(x => x.Id==7)
                            select new
                            {
                                id = s.Id,
                                name=s.Name,
                                content = s.Content
                            }).Take(4).ToList();
                return Json(new { code = 200, w = w, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult FooterRight()
        {
            try
            {
                var footerright = (from tt in db.Modules.Where(x => x.Id == 14 ||
                                                                x.Id == 15 )
                                   select new
                                   {
                                       id = tt.Id,
                                       name = tt.Name,
                                       phone = tt.Phone,
                                       email=tt.Email,
                                       address = tt.Address,
                                   }).ToList();
                var footerright2 = (from r2 in db.Modules.Where(x => x.Id == 16 ||
                                                                x.Id == 17 ||
                                                                x.Id == 18)
                                    select new
                                    {
                                        id = r2.Id,
                                        title = r2.Title,
                                        link = r2.Link,
                                        dec = r2.Description
                                    }).ToList();
                return Json(new { code = 200, footerright2= footerright2, footerright=footerright , msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult PartnerContent()
        {
            try
            {
                var w = (from s in db.Modules.Where(x => x.Id == 19)
                         select new
                         {
                             id = s.Id,
                             name = s.Name,
                             content = s.Content
                         }).ToList();
                return Json(new { code = 200, w = w, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}