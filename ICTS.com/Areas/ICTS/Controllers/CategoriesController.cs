using ICTS.com.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ICTS.com.Areas.ICTS.Controllers
{
    [HandleError]
    public class CategoriesController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
        // GET: ICTS/Categories
        private Entities db = new Entities();
        public string UploadImage( HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                   var now = DateTime.Now.ToString().Trim();
                    var index1 = now.IndexOf(" ");
                    var sub1 =now.Substring(0,index1);
                    var sub11 = sub1.Replace("/","");
                    var index2 = now.IndexOf(" ", index1 + 1);
                    var sub2 = now.Substring(index1+1);
                    var sub21 = sub2.Replace(":", "");
                    string _FileName = "";
                    int index = file.FileName.IndexOf('.');
                    _FileName = sub11 + sub21 + "Category" + file.FileName;
                    file.SaveAs(Server.MapPath("/Images/" + _FileName));

                    return "/Images/" + _FileName;
                }

            }
            return "";
           
        }
        [HttpGet]
        public JsonResult DanhMucSanPham(string seach, int page)
        {
            try
            {
                var pageSize = 5;
                var dmspp = (from s in db.Categories.Where(x => x.Id > 0)
                             select new
                             {
                                 id = s.Id,
                                 name = s.Name,
                                 title = s.Title,
                                 image = s.ImageId,
                                 createdate = s.CreateDate.Value.Day + "/" + s.CreateDate.Value.Month + "/" + s.CreateDate.Value.Year,
                                 createby = s.CreateBy,
                                 modifiledate = s.ModifileDate.Value.Day + "/" + s.ModifileDate.Value.Month + "/" + s.ModifileDate.Value.Year,
                                 modifileby = s.ModifileBy,
                                 status = s.Status == true ? "Hiển thị" : "Không hiển thị",
                                 statusimage = s.StatusImage == true ? "Hiển thị" : "Không hiển thị"
                             }).ToList().Where(x => x.status.ToLower().Contains(seach) || x.name.ToLower().Contains(seach) ||
                                            x.createby.ToLower().Contains(seach) || x.createdate.ToLower().Contains(seach) ||
                                            x.modifileby.ToLower().Contains(seach) || x.modifiledate.ToLower().Contains(seach)||
                                            x.statusimage.ToLower().Contains(seach));
                var pages = dmspp.Count() % pageSize == 0 ? dmspp.Count() / pageSize : dmspp.Count() / pageSize + 1;
                var dmsp = dmspp.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                return Json(new { code = 200, pages = pages, dmsp = dmsp, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult ProductCategoryById()
        {
            try
            {
                var dmsp = (from s in db.Categories.Where(x => x.Id > 0)
                             select new
                             {
                                 id = s.Id,
                                 name = s.Name,
                                 title = s.Title,
                                 image = s.ImageId,
                                 createdate = s.CreateDate.Value.Day + "/" + s.CreateDate.Value.Month + "/" + s.CreateDate.Value.Year,
                                 createby = s.CreateBy,
                                 modifiledate = s.ModifileDate.Value.Day + "/" + s.ModifileDate.Value.Month + "/" + s.ModifileDate.Value.Year,
                                 modifileby = s.ModifileBy,
                                 status = s.Status == true ? "Hiển thị" : "Không hiển thị",
                                 statusimage = s.StatusImage == true ? "Hiển thị" : "Không hiển thị"
                             }).ToList();
                return Json(new { code = 200,dmsp = dmsp, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult AddDMSP(string name, string url, string title, bool status,bool statusimage, string image)
        {
            try
            {
                var session = (Admin)Session["admin"];
                var nameAdmin = session.Name;
                var d = new Category();
                var images = image.Substring(8);
                d.ImageId = images;
                d.Name = name;
                d.Meta = url;
                d.CreateBy = nameAdmin;
                d.CreateDate = DateTime.Now;
                d.ModifileBy = nameAdmin;
                d.ModifileDate = DateTime.Now;
                d.Title = title;
                d.Status = status;
                d.StatusImage = statusimage;
                db.Categories.Add(d);
                db.SaveChanges();
                return Json(new { code = 200, msg = "Thêm danh sách thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, mmsg = "Thêm danh sách không thành công" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult Edit(int id,string name, string url, string title, bool status,bool statusimage, string image)
        {
            try
            {
                var images = image.Substring(8);
                var session = (Admin)Session["admin"];
                var nameAdmin = session.Name;
                var dmsp = db.Categories.SingleOrDefault(x => x.Id == id);
                dmsp.Name = name;
                dmsp.Meta = url;
                dmsp.Title = title;
                dmsp.Status = status;
                dmsp.StatusImage = statusimage;
                dmsp.ImageId = images;
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
                var tv = db.Categories.SingleOrDefault(x => x.Id == id);
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
                var l = db.Categories.Find(id);
                var session = (Admin)Session["admin"];
                var role = session.Role;
                if (role == true)
                {
                    db.Categories.Remove(l);
                    db.SaveChanges();
                    return Json(new { code = 200, msg = "Xoa Dữ Liệu thành công" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { code = 300, msg = "Bạn không có quyền xóa dữ liệu" }, JsonRequestBehavior.AllowGet);
                }
               
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Xoa nhật dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult SeachAuto(string seach)
        {
            try
            {
                var dmsp = (from s in db.Categories.Where(x => x.Id > 0)
                             select new
                             {
                                 id = s.Id,
                                 name = s.Name
                              
                             }).ToList().Where(x => x.name.ToLower().Contains(seach));
                return Json(new { code = 200,dmsp = dmsp, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}