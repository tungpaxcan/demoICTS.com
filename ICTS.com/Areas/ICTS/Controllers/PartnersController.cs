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
    public class PartnersController : Controller
    {
        private Entities db = new Entities();
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
                    var sub2 = now.Substring(index1 + 1);
                    var sub21 = sub2.Replace(":", "");
                    string _FileName = "";
                    int index = file.FileName.IndexOf('.');
                    _FileName = sub11 + sub21 + "partner" + "." + file.FileName.Substring(index + 1);
                    file.SaveAs(Server.MapPath("/Images/" + _FileName));

                    return "/Images/" + _FileName;
                }

            }
            return "";

        }
        [HttpGet]
        public JsonResult Partner(string seach, int page)
        {
            try
            {
                var pageSize = 10;
                var pn = (from s in db.Partners.Where(x => x.Id > 0)
                          select new
                          {
                              id = s.Id,
                              name = s.Name,
                              title = s.Title,
                              image = s.Image,
                              link = s.Link,
                              createdate = s.CreateDate.Value.Day + "/" + s.CreateDate.Value.Month + "/" + s.CreateDate.Value.Year,
                              createby = s.CreateBy,
                              modifiledate = s.ModifileDate.Value.Day + "/" + s.ModifileDate.Value.Month + "/" + s.ModifileDate.Value.Year,
                              modifileby = s.ModifileBy,
                              status = s.Status == true ? "Hiển thị" : "Không hiển thị"
                          }).ToList().Where(x=>x.status.ToLower().Contains(seach)||x.name.ToLower().Contains(seach)||
                                            x.createby.ToLower().Contains(seach)||x.createdate.ToLower().Contains(seach)||
                                            x.modifileby.ToLower().Contains(seach)||x.modifiledate.ToLower().Contains(seach));
                var pages = pn.Count() % pageSize == 0 ? pn.Count() / pageSize : pn.Count() / pageSize + 1;
                var pnt = pn.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                return Json(new { code = 200, pages = pages, pnt = pnt, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult Add(string name, string image, string title, bool status, string link)
        {
            try
            {
                var session = (Admin)Session["admin"];
                var nameAdmin = session.Name;
                var d = new Partner();
                var images = image.Substring(8);
                d.Image = images;
                d.Name = name;
                d.Title = title;
                d.Link = link;
                d.CreateBy = nameAdmin;
                d.CreateDate = DateTime.Now;
                d.ModifileBy = nameAdmin;
                d.ModifileDate = DateTime.Now;
                d.Status = status;
                db.Partners.Add(d);
                db.SaveChanges();
                return Json(new { code = 200, msg = "Thêm danh sách thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, mmsg = "Thêm danh sách không thành công" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult Edit(int id, string name, string title, string link, bool status, string image)
        {
            try
            {
                var images = image.Substring(8);
                var session = (Admin)Session["admin"];
                var nameAdmin = session.Name;
                var pn = db.Partners.SingleOrDefault(x => x.Id == id);
                pn.Name = name;
                pn.Link = link;
                pn.Title = title;
                pn.Status = status;
                pn.Image = images;
                pn.ModifileBy = nameAdmin;
                pn.ModifileDate = DateTime.Now;
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
                var tv = db.Partners.SingleOrDefault(x => x.Id == id);
                return Json(new { code = 200, tv = tv, msg = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                var l = db.Partners.Find(id);
                var session = (Admin)Session["admin"];
                var role = session.Role;
                if (role == true)
                {
                    db.Partners.Remove(l);
                    db.SaveChanges();
                    return Json(new { code = 200, msg = "Xoa Dữ Liệu thành công" }, JsonRequestBehavior.AllowGet);
                }
                else{
                    return Json(new { code = 300, msg = "Bạn không có quyền xóa dữ liệu" }, JsonRequestBehavior.AllowGet);
                }
                
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Xoa nhật dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
