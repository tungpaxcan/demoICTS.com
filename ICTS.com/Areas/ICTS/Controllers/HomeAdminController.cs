using ICTS.com.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ICTS.com.Areas.ICTS.Controllers
{
    [HandleError]
    public class HomeAdminController : BaseController
    {
        // GET: ICTS/HomeAdmin
        private Entities db = new Entities();
        // GET: Admin/HomeAdmin
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
                    _FileName = "Logo" + id.ToString() + "." + file.FileName.Substring(index + 1);
                    file.SaveAs(Server.MapPath("/Images/" + _FileName));

                    return "/Images/" + _FileName;
                }

            }
            return "";

        }
        [HttpGet]
        public JsonResult DetailLogo(int id)
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
        public JsonResult EditLogo(int id, string image)
        {
            try
            {
                var images = image.Substring(8);
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
        [HttpGet]
        public JsonResult CSKH()
        {
            try
            {
                var ttlh = (from tt in db.Modules.Where(x => x.Id == 1 || x.Id == 2 || x.Id == 3 || x.Id == 4)
                            select new
                            {
                                id = tt.Id,
                                title = tt.Title,
                                name = tt.Name,
                                phone = tt.Phone,
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
        public JsonResult WhyChoose()
        {
            try
            {
                var ct = (from tt in db.Modules.Where(x => x.Id == 7)
                            select new
                            {
                                id = tt.Id,
                                content=tt.Content,
                                description = tt.Description,
                                name = tt.Name,
                                createdate = tt.CreateDate,
                                createby = tt.CreateBy,
                                modifiledate = tt.ModifileDate,
                                modifileby = tt.ModifileBy
                            }).ToList();
                return Json(new { code = 200, ct = ct, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult DetailWhy(int id)
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
        [HttpPost, ValidateInput(false)]
        public JsonResult EditWhy(int id, string name,string content)
        {
            try
            {
                var session = (Admin)Session["admin"];
                var nameAdmin = session.Name;
                var why = db.Modules.SingleOrDefault(x => x.Id == id);
                why.ModifileBy = nameAdmin;
                why.ModifileDate = DateTime.Now;
                why.Name = name;
                why.Content = content;
                db.SaveChanges();
                return Json(new { code = 200, msg = "Sửa danh sách thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, mmsg = "Sửa danh sách không thành công" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult DetailCSKH(int id)
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                var ttlh = db.Modules.SingleOrDefault(x => x.Id == id);
                return Json(new { code = 200, ttlh = ttlh, msg = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, mmsg = "" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult EditCSKH(int id, string ten, string meta, string phone)
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                var ttlh = db.Modules.SingleOrDefault(x => x.Id == id);
                ttlh.Name = ten;
                ttlh.Meta = meta;
                ttlh.Phone = phone;
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