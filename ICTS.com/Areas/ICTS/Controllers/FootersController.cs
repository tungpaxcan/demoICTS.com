using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ICTS.com.Models;

namespace ICTS.com.Areas.ICTS.Controllers
{
    public class FootersController : Controller
    {
        private Entities db = new Entities();
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
                    _FileName = sub11 + sub21 + "profile" + "." + file.FileName.Substring(index + 1);
                    file.SaveAs(Server.MapPath("/Images/" + _FileName));

                    return "/Images/" + _FileName;
                }

            }
            return "";

        }
        // GET: ICTS/Footers
        [HttpGet]
        public JsonResult FooterLeft()
        {
            try
            {
                var footerleft = (from tt in db.Modules.Where(x => x.Id == 8 ||
                                                        x.Id == 9 ||
                                                        x.Id == 10 ||
                                                        x.Id == 11||
                                                        x.Id==12)
                            select new
                            {
                                id = tt.Id,
                                name = tt.Name,
                                meta = tt.Meta,
                                link =tt.Link,
                                modifileBy = tt.ModifileBy,
                                modifileDate = tt.ModifileDate.Value.Day + "/" + tt.ModifileDate.Value.Month + "/" + tt.ModifileDate.Value.Year,
                            }).ToList();
                return Json(new { code = 200, footerleft = footerleft, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult Edit(int id, string name,string meta)
        {
            try
            {
                var session = (Admin)Session["admin"];
                var nameAdmin = session.Name;
                var f = db.Modules.SingleOrDefault(x => x.Id == id);
                f.Name = name;
                f.Meta = meta;
                f.ModifileBy = nameAdmin;
                f.ModifileDate = DateTime.Now;
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
                var tv = db.Modules.SingleOrDefault(x => x.Id == id);
                return Json(new { code = 200, tv = tv, msg = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult FooterCenter()
        {
            try
            {
                var footercenter = (from tt in db.Modules.Where(x => x.Id == 13)
                                  select new
                                  {
                                      id = tt.Id,
                                      image = tt.Image,
                                      modifileBy = tt.ModifileBy,
                                      modifileDate = tt.ModifileDate.Value.Day + "/" + tt.ModifileDate.Value.Month + "/" + tt.ModifileDate.Value.Year,
                                  }).ToList();
                return Json(new { code = 200, footercenter = footercenter, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult EditCenter(int id, string image)
        {
            try
            {
                var images = image.Substring(8);
                var session = (Admin)Session["admin"];
                var nameAdmin = session.Name;
                var f = db.Modules.SingleOrDefault(x => x.Id == id);
                f.Image = images;
                f.ModifileBy = nameAdmin;
                f.ModifileDate = DateTime.Now;
                db.SaveChanges();
                return Json(new { code = 200, msg = "Sửa danh sách thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, mmsg = "Sửa danh sách không thành công" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult DetailCenter(int id)
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
        [HttpGet]
        public JsonResult FooterRight()
        {
            try
            {
                var footerright = (from tt in db.Modules.Where(x => x.Id == 14 ||
                                                                x.Id == 15 ||
                                                                x.Id == 16 ||
                                                                x.Id == 17 ||
                                                                x.Id == 18 )
                                    select new
                                    {
                                        id = tt.Id,
                                        meta = tt.Meta,
                                        title = tt.Title,
                                        name = tt.Name,
                                        phone = tt.Phone,
                                        description = tt.Description,
                                        address = tt.Address,
                                        link=tt.Link,
                                        email = tt.Email,
                                        modifileBy = tt.ModifileBy,
                                        modifileDate = tt.ModifileDate.Value.Day + "/" + tt.ModifileDate.Value.Month + "/" + tt.ModifileDate.Value.Year,
                                    }).ToList();
                return Json(new { code = 200, footerright = footerright, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult EditRight(int id, string name,string dec,string phone,string email,string link,string address)
        {
            try
            {
                var session = (Admin)Session["admin"];
                var nameAdmin = session.Name;
                var f = db.Modules.SingleOrDefault(x => x.Id == id);
                f.Name = name;
                f.Description = dec;
                f.Phone = phone;
                f.Email = email;
                f.Link = link;
                f.Address = address;
                f.ModifileBy = nameAdmin;
                f.ModifileDate = DateTime.Now;
                db.SaveChanges();
                return Json(new { code = 200, msg = "Sửa danh sách thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, mmsg = "Sửa danh sách không thành công" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult DetailRight(int id)
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
    }
}