using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ICTS.com.Models;

namespace ICTS.com.Areas.ICTS.Controllers
{
    public class ImageAddsController : Controller
    {
        private Entities db = new Entities();
        // GET: ICTS/ImageAdds
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
                    _FileName = sub11 + sub21 + "Product" + file.FileName;
                    file.SaveAs(Server.MapPath("/Images/" + _FileName));

                    return "/Images/" + _FileName;
                }

            }
            return "";

        }
        [HttpGet]
        public JsonResult Id(int id)
        {
            try
            {
                var dmsp = (from s in db.Images.Where(x => x.IdProduct == id)
                            select new
                            {
                                id=s.Id,
                                image = s.Image1,
                                idproduct = s.IdProduct
                            }).ToList();
                return Json(new { code = 200, dmsp = dmsp, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult Add(int id, string image)
        {
            try
            {
                var images = image.Substring(8);
                var session = (Admin)Session["admin"];
                var nameAdmin = session.Name;
                var d = new Image();
                d.IdProduct = id;
                d.Image1 = images;
                d.CreateBy = nameAdmin;
                d.CreateDate = DateTime.Now;
                d.ModifileBy = nameAdmin;
                d.ModifileDate = DateTime.Now;
                db.Images.Add(d);
                db.SaveChanges();
                return Json(new { code = 200, msg = "Thêm danh sách thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, mmsg = "Thêm danh sách không thành công" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                var l = db.Images.Find(id);
                var session = (Admin)Session["admin"];
                var role = session.Role;
                if (role == true)
                {
                    db.Images.Remove(l);
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