using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ICTS.com.Models;

namespace ICTS.com.Areas.ICTS.Controllers
{
    public class CustomersController : Controller
    {
        private Entities db = new Entities();
        // GET: ICTS/Customers
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
                    _FileName = sub11 + sub21 + "Customer" + file.FileName;
                    file.SaveAs(Server.MapPath("/Images/" + _FileName));

                    return "/Images/" + _FileName;
                }

            }
            return "";

        }
        [HttpGet]
        public JsonResult Customer()
        {
            try
            {
                var dmsp = (from s in db.Customers.Where(x => x.Id > 0)
                            select new
                            {
                                id = s.Id,
                                name = s.Name,
                                title = s.Title,
                                meta = s.Meta,
                                link=s.link,
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
        public JsonResult Add(string name, string url, string title,string link, bool status, bool statusimage, string image)
        {
            try
            {
                var session = (Admin)Session["admin"];
                var nameAdmin = session.Name;
                var d = new Customer();
                var images = image.Substring(8);
                d.Image = images;
                d.Name = name;
                d.Meta = url;
                d.Title = title;
                d.link = link;
                d.CreateBy = nameAdmin;
                d.CreateDate = DateTime.Now;
                d.ModifileBy = nameAdmin;
                d.ModifileDate = DateTime.Now;
                d.Status = status;
                d.StatusImage = statusimage;
                db.Customers.Add(d);
                db.SaveChanges();
                return Json(new { code = 200, msg = "Thêm danh sách thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, mmsg = "Thêm danh sách không thành công" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost, ValidateInput(false)]
        public JsonResult Edit(int id, string name, string url, string title,string link, bool status, bool statusimage, string image)
        {
            try
            {
                var images = image.Substring(8);
                var session = (Admin)Session["admin"];
                var nameAdmin = session.Name;
                var dmsp = db.Customers.SingleOrDefault(x => x.Id == id);
                dmsp.Name = name;
                dmsp.Meta = url;
                dmsp.Title = title;
                dmsp.link = link;
                dmsp.Status = status;
                dmsp.StatusImage = statusimage;
                dmsp.Image = images;
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
                var tv = db.Customers.SingleOrDefault(x => x.Id == id);
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
                var l = db.Customers.Find(id);
                var session = (Admin)Session["admin"];
                var role = session.Role;
                if (role == true)
                {
                    db.Customers.Remove(l);
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
    }
}