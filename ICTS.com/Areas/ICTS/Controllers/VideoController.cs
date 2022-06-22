using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ICTS.com.Models;
namespace ICTS.com.Areas.ICTS.Controllers
{
    public class VideoController : Controller
    {
        private Entities db = new Entities();
        // GET: ICTS/Video
        public ActionResult Index()
        {
            return View();
        }
        public string UploadImage(HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    int id = 0;
                    string _FileName = "";
                    int index = file.FileName.IndexOf('.');
                    _FileName = "video" + id.ToString() + "." + file.FileName.Substring(index + 1);
                    file.SaveAs(Server.MapPath("/Images/" + _FileName));

                    return "/Images/" + _FileName;
                }

            }
            return "";

        }
        [HttpGet]
        public JsonResult Video()
        {
            try
            {
                var ttlh = (from tt in db.Modules.Where(x => x.Id == 20)
                            select new
                            {
                                id = tt.Id,
                                title = tt.Title,
                                name = tt.Name,
                                video = tt.Image,
                                meta = tt.Meta
                            }).ToList();
                return Json(new { code = 200, ttlh = ttlh, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult Detail(int id)
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                var tv = db.Modules.SingleOrDefault(x => x.Id == id);
                return Json(new { code = 200, tv = tv, msg = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult Edit(int id, string video)
        {
            try
            {
                var images = video.Substring(8);
                var session = (Admin)Session["admin"];
                var nameAdmin = session.Name;
                var logo = db.Modules.SingleOrDefault(x => x.Id == id);
                logo.Image = images;
                logo.ModifileBy = nameAdmin;
                logo.ModifileDate = DateTime.Now;
                db.SaveChanges();
                return Json(new { code = 200, msg = "Sửa danh sách thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, mmsg = "Sửa danh sách không thành công" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}