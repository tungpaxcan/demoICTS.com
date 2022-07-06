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
    public class BlogAddController : Controller
    {
        private Entities db = new Entities();
        // GET: BlogAdd
        public ActionResult Index(string meta,int id)
        {

            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogAdd blogadd = db.BlogAdds.Find(id);
            if (blogadd == null)
            {
                return HttpNotFound();
            }
            return View(blogadd);
        }
        [HttpGet]
        public JsonResult BlogAddUrl(int id)
        {
            try
            {
                var left = (from s in db.BlogAdds.Where(x => x.Status != false && x.Id == id)
                            select new
                            {
                                id = s.Id,
                                title = s.Title,
                                meta = s.Meta,
                                name = s.Name,
                                content = s.Content,
                                image = s.Image
                            }).ToList();
                return Json(new { code = 200, left = left, Url = "/blog/chi-tiet/" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult BlogAdd(int id)
        {
            try
            {
                var left = (from s in db.BlogAdds.Where(x => x.Status != false && x.Id == id)
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
    }
}