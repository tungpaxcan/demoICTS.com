using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ICTS.com.Models;

namespace ICTS.com.Areas.ICTS.Controllers
{
    public class PromotionController : Controller
    {
        private Entities db = new Entities();
        // GET: ICTS/Promotion
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
                    var now = DateTime.Now.ToString().Trim();
                    var index1 = now.IndexOf(" ");
                    var sub1 = now.Substring(0, index1);
                    var sub11 = sub1.Replace("/", "");
                    var index2 = now.IndexOf(" ", index1 + 1);
                    var sub2 = now.Substring(index1 + 1);
                    var sub21 = sub2.Replace(":", "");
                    string _FileName = "";
                    int index = file.FileName.IndexOf('.');
                    _FileName = sub11 + sub21 + "Promotion" + file.FileName;
                    file.SaveAs(Server.MapPath("/Images/" + _FileName));

                    return "/Images/" + _FileName;
                }

            }
            return "";

        }
        [HttpGet]
        public JsonResult Promotion()
        {
            try
            {
                var dmsp = (from s in db.Promotions.Where(x => x.Id > 0)
                             select new
                             {
                                 id = s.Id,
                                 name = s.Name,
                                 title = s.Title,
                                 meta = s.Meta,
                                 content = s.Content,
                                 image = s.Image,
                                 createdate = s.CreateDate.Value.Day + "/" + s.CreateDate.Value.Month + "/" + s.CreateDate.Value.Year,
                                 createby = s.CreateBy,
                                 modifiledate = s.ModifileDate.Value.Day + "/" + s.ModifileDate.Value.Month + "/" + s.ModifileDate.Value.Year,
                                 modifileby = s.ModifileBy,
                                 status = s.Status == true ? "Hiển thị" : "Không hiển thị",
                                 statusimage = s.StatusImage == true ? "Hiển thị" : "Không hiển thị"
                             }).ToList();
                return Json(new { code = 200, dmsp = dmsp, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost, ValidateInput(false)]
        public JsonResult Add(string name, string url, string title, string content, bool status, bool statusimage, string image)
        {
            try
            {
                var session = (Admin)Session["admin"];
                var nameAdmin = session.Name;
                var d = new Promotion();
                var images = image.Substring(8);
                d.Image = images;
                d.Name = name;
                d.Meta = url;
                d.Title = title;
                d.CreateBy = nameAdmin;
                d.CreateDate = DateTime.Now;
                d.ModifileBy = nameAdmin;
                d.ModifileDate = DateTime.Now;
                d.Content = content;
                d.Status = status;
                d.StatusImage = statusimage;
                db.Promotions.Add(d);
                db.SaveChanges();
                return Json(new { code = 200, msg = "Thêm danh sách thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, mmsg = "Thêm danh sách không thành công" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost, ValidateInput(false)]
        public JsonResult Edit(int id, string name, string url, string title, bool status, bool statusimage, string image, string content)
        {
            try
            {
                var images = image.Substring(8);
                var session = (Admin)Session["admin"];
                var nameAdmin = session.Name;
                var dmsp = db.Promotions.SingleOrDefault(x => x.Id == id);
                dmsp.Name = name;
                dmsp.Meta = url;
                dmsp.Title = title;
                dmsp.Status = status;
                dmsp.StatusImage = statusimage;
                dmsp.Image = images;
                dmsp.Content = content;
                dmsp.ModifileBy = nameAdmin;
                dmsp.ModifileDate = DateTime.Now;
                db.SaveChanges();
                return Json(new { code = 200, msg = "Sửa danh sách thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, mmsg = "Sửa danh sách không thành công" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult Detail(int id)
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                var tv = db.Promotions.SingleOrDefault(x => x.Id == id);
                return Json(new { code = 200, tv = tv, msg = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost, ValidateInput(false)]
        public JsonResult Delete(int id)
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                var l = db.Promotions.Find(id);
                db.Promotions.Remove(l);
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