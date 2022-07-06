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
    public class BlogController : Controller
    {
        private Entities db = new Entities();
        // GET: Blog
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Detail(string meta, int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }
        [HttpGet]
        public JsonResult BlogMenu()
        {
            try
            {

                var dmsp = (from s in db.Blogs.Where(x => x.Status != false)
                            orderby s.CreateDate descending
                            select new
                            {
                                id = s.Id,
                                meta = s.Meta,
                                title = s.Title,
                                name = s.Name,
                                image = s.Image,
                                content = s.Content
                            }).ToList();
                return Json(new { code = 200, dmsp = dmsp, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult BlogImage()
        {
            try
            {

                var dmsp = (from s in db.Blogs.Where(x => x.StatusImage != false)
                            orderby s.CreateDate descending
                            select new
                            {
                                id = s.Id,
                                meta = s.Meta,
                                title = s.Title,
                                name = s.Name,
                                image = s.Image,
                                content = s.Content
                            }).ToList();
                return Json(new { code = 200, dmsp = dmsp, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult BlogUrl(int id)
        {
            try
            {
                var left = (from s in db.Blogs.Where(x => x.Status != false && x.Id == id)
                            select new
                            {
                                id = s.Id,
                                title = s.Title,
                                meta = s.Meta,
                                name = s.Name,
                                content = s.Content,
                                image = s.Image
                            }).ToList();
                return Json(new { code = 200, left = left, Url = "/blog/danh-muc/" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult BlogAddById(int id)
        {
            try
            {
                var left = (from s in db.BlogAdds.Where(x => x.Status != false && x.IdBlog == id)
                            join ss in db.Blogs on s.IdBlog equals ss.Id
                            select new
                            {
                                id = s.Id,
                                title = s.Title,
                                meta = s.Meta,
                                name = s.Name,
                                content = s.Content,
                                image = s.Image
                            }).ToList();
                return Json(new { code = 200, left = left, msg = "Hiểm thị dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult BlogAddOrder(int id)
        {
            try
            {
                var left = (from s in db.Blogs.Where(x => x.Status != false && x.Id != id)
                            select new
                            {
                                id = s.Id,
                                title = s.Title,
                                meta = s.Meta,
                                name = s.Name,
                                content = s.Content,
                                image = s.Image
                            }).ToList();
                return Json(new { code = 200, left = left, msg = "Hiểm thị dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}