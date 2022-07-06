using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ICTS.com.Models;

namespace ICTS.com.Controllers
{
    public class ServiceController : Controller
    {
        private Entities db = new Entities();
        // GET: Service
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Detail(string meta,int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }
        [HttpGet]
        public JsonResult ServiceMenu()
        {
            try
            {

                var dmsp = (from s in db.Services.Where(x => x.Status != false)
                            orderby s.CreateDate descending
                            select new
                            {
                                id=s.Id,
                                meta=s.Meta,
                                title=s.Title,
                                name=s.Name,
                                image=s.Image,
                                content=s.Content
                            }).ToList();
                return Json(new { code = 200, dmsp = dmsp, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult ServiceDetail(int id)
        {
            try
            {

                var dmsp = (from s in db.Services.Where(x => x.Id == id)
                            select new
                            {
                                id = s.Id,
                                meta = s.Meta.Replace("&",""),
                                title = s.Title,
                                name = s.Name,
                                image = s.Image,
                                content=s.Content
                            }).ToList();
                return Json(new { code = 200, dmsp = dmsp,Url="/dich-vu/chi-tiet-dich-vu/", msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult ServiceDetailOther(int id)
        {
            try
            {

                var dmsp = (from s in db.Services.Where(x => x.Id != id)
                            select new
                            {
                                id = s.Id,
                                meta = s.Meta.Replace("&", ""),
                                title = s.Title,
                                name = s.Name,
                                image = s.Image,
                                content = s.Content
                            }).ToList();
                return Json(new { code = 200, dmsp = dmsp, Url = "/dich-vu/chi-tiet-dich-vu/", msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult ServiceImage()
        {
            try
            {

                var dmsp = (from s in db.Services.Where(x => x.StatusImage != false)
                            orderby s.CreateBy descending
                            select new
                            {
                                id = s.Id,
                                meta = s.Meta,
                                title = s.Title,
                                name = s.Name,
                                image = s.Image
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