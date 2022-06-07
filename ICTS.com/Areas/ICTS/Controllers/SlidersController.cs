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

namespace ICTS.com.Areas.ICTS.Controllers
{
    public class SlidersController : Controller
    {
        private Entities db = new Entities();

        // GET: ICTS/Sliders
        public ActionResult Slider()
        {
            return View();
        }
        [HttpGet]
        public JsonResult DMSlider()
        {
            try
            {
                var slider = (from tt in db.Sliders.Where(x => x.Id > 0)
                            select new
                            {
                                id = tt.Id,
                                title = tt.Title,
                                name = tt.Name,
                                image=tt.Image,
                                createdate = tt.CreateDate,
                                createby = tt.CreateBy,
                                modifiledate = tt.ModifileDate,
                                modifileby=tt.ModifileBy,
                                status=tt.Status == true ? "Hiển Thị" : "Không Hiển Thị"
                            }).ToList();
                return Json(new { code = 200, slider = slider, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiển thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public string UploadImage(HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    var now = DateTime.Now.ToString().Trim();
                    var index1 = now.IndexOf(" ");
                    var sub1 = now.Substring(0, index1);
                    var sub11 = sub1.Replace("/", "");
                    var index2 = now.IndexOf(" ", index1 + 1);
                    var sub2 = now.Substring(index1 + 1);
                    var sub21 = sub2.Replace(":", "");
                    string _FileName = "";
                    int index = file.FileName.IndexOf('.');
                    _FileName =sub11+sub21+ "Slider"  + "." + file.FileName.Substring(index + 1);
                    file.SaveAs(Server.MapPath("/Images/" + _FileName));
                    return "/Images/" + _FileName;
                }
            }
            return "";

        }
        [HttpGet]
        public JsonResult Detail(int id)
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                var tv = db.Sliders.SingleOrDefault(x => x.Id == id);
                return Json(new { code = 200, tv = tv, msg = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult Add(string name, string title, bool status, string image, HttpPostedFileBase file)
        {
            try
            {
                var session = (Admin)Session["admin"];
                var nameAdmin = session.Name;
                var d = new Slider();
                var images = image.Substring(8);
                d.Image = images;
                d.Name = name;
                d.CreateBy = nameAdmin;
                d.CreateDate = DateTime.Now;
                d.Title = title;
                d.Status = status;
                db.Sliders.Add(d);
                db.SaveChanges();
                return Json(new { code = 200, msg = "Thêm danh sách thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, mmsg = "Thêm danh sách không thành công" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult Edit(int id, string name, string title, bool status, string image)
        {
            try
            {
                var images = image.Substring(8);
                var session = (Admin)Session["admin"];
                var nameAdmin = session.Name;
                var sl = db.Sliders.SingleOrDefault(x => x.Id == id);
                sl.Name = name;
                sl.Title = title;
                sl.Status = status;
                sl.Image = images;
                sl.ModifileBy = nameAdmin;
                sl.ModifileDate = DateTime.Now;
                db.SaveChanges();
                return Json(new { code = 200, msg = "Sửa danh sách thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, mmsg = "Sửa danh sách không thành công" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                var l = db.Sliders.Find(id);
                db.Sliders.Remove(l);
                db.SaveChanges();
                return Json(new { code = 200, msg = "Xoa Dữ Liệu thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Xoa nhật dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
