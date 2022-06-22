using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ICTS.com.Models;
namespace ICTS.com.Controllers
{
    [HandleError]
    public class ContactController : Controller
    {
        private Entities db = new Entities();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult IdProduct()
        {
            try
            {
                var a = (from s in db.Products.Where(x => x.Id >0)
                         select new
                         {
                             id = s.Id,
                             title = s.Title
                         }).Take(1).ToList();
                return Json(new { code = 200, a = a, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult CSKH()
        {
            try
            {
                var a = (from s in db.Modules.Where(x => x.Id == 3)
                            select new
                            {
                                id = s.Id,
                                title = s.Title,
                                phone=s.Phone
                            }).ToList();
                return Json(new { code = 200, a = a, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult KT()
        {
            try
            {
                var a = (from s in db.Modules.Where(x => x.Id == 4)
                         select new
                         {
                             id = s.Id,
                             title = s.Title,
                             phone = s.Phone
                         }).ToList();
                return Json(new { code = 200, a = a, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult PhoneHCM()
        {
            try
            {
                var a = (from s in db.Modules.Where(x => x.Id == 1)
                         select new
                         {
                             id = s.Id,
                             title = s.Title,
                             phone = s.Phone
                         }).ToList();
                return Json(new { code = 200, a = a, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult PhoneHN()
        {
            try
            {
                var a = (from s in db.Modules.Where(x => x.Id == 2)
                         select new
                         {
                             id = s.Id,
                             title = s.Title,
                             phone = s.Phone
                         }).ToList();
                return Json(new { code = 200, a = a, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult TPHCM()
        {
            try
            {
                var a = (from s in db.Modules.Where(x => x.Id == 14)
                         select new
                         {
                             id = s.Id,
                             title = s.Title,
                             address= s.Address,
                             email=s.Email
                         }).ToList();
                return Json(new { code = 200, a = a, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult HN()
        {
            try
            {
                var a = (from s in db.Modules.Where(x => x.Id == 15)
                         select new
                         {
                             id = s.Id,
                             title = s.Title,
                             address = s.Address
                         }).ToList();
                return Json(new { code = 200, a = a, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult Video()
        {
            try
            {
                var a = (from s in db.Modules.Where(x => x.Id == 20)
                         select new
                         {
                             id = s.Id,
                             title = s.Title,
                             video = s.Image
                         }).ToList();
                return Json(new { code = 200, a = a, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        // GET: Contact
        [HttpPost]
        public JsonResult Add(int id,string name, string phone, string email, string enterprise, int amount,string dec)
        {
            try
            {
                var d = new Contact();
                d.Name = name;
                d.IdProduct = id;
                d.Phone = phone;
                d.Email = email;
                d.CreateDate = DateTime.Now;
                d.ModifileDate = DateTime.Now;
                d.NameEnterprise = enterprise;
                d.Amount = amount;
                d.Description = dec;
                db.Contacts.Add(d);
                db.SaveChanges();
                return Json(new { code = 200, msg = "Thêm danh sách thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, mmsg = "Thêm danh sách không thành công" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}