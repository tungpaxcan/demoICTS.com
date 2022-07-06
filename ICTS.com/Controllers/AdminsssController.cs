using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ICTS.com.Models;

namespace ICTS.com.Controllers
{
    public class AdminsssController : Controller
    {
        private Entities db = new Entities();
        // GET: Adminsss
        [HttpGet]
        public JsonResult Add()
        {
            try
            {
                var a =(Admin)Session["admin"];
                var d = a.Name;
                var dfirst = a.Name.Substring(0, 1);
                var email = a.Email;
                var id = a.Id;
                var phone = a.Phone;
                var address = a.Address;
                var role = a.Role;
                var user = a.Users;
                var pass = a.Pass;
                var date = DateTime.Now;
                return Json(new { code = 200,phone=phone,address=address,role=role,user=user,pass=pass,id=id,email=email ,date = date, d = d,dfirst=dfirst, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult SignOut()
        {
            try
            {
                Session["admin"] = null;
                return Json(new { code = 200,Url="/ICTS/Default/Login", }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult Edit(string pass)
        {
            try
            {
                var a = (Admin)Session["admin"];
                
                a.Pass = pass;
                var dmsp = db.Admins.SingleOrDefault(x => x.Id == a.Id);
                dmsp.Pass = pass;
                db.SaveChanges();
                return Json(new { code = 200, msg="Ok"}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult Addd(string pass,string name,string user,string email,int phone,string address,bool role)
        {
            try
            {
                var a = (Admin)Session["admin"];

                a.Pass = pass;
                var d = new Admin();
                d.Pass = pass;
                d.Name = name;
                d.Users = user;
                d.Email = email;
                d.Phone = phone;
                d.Address = address;
                d.Role = role;
                db.Admins.Add(d);
                db.SaveChanges();
                return Json(new { code = 200, msg = "Ok" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult Admin()
        {
            try
            {
                var a = (from s in db.Admins.Where(x => x.Id > 0)
                         select new
                         {
                             id=s.Id,
                             user = s.Users,
                             pass=s.Pass,
                             name=s.Name,
                             role=s.Role ==true?"Admin":"Quản Lý"
                         });

                return Json(new { code = 200, msg = "Hiểm thị dữ liệu thất bại",a=a }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                var a = db.Admins.SingleOrDefault(x => x.Id == id);
                db.Admins.Remove(a);
                db.SaveChanges();
                return Json(new { code = 200, msg = "Hiểm thị dữ liệu thất bại"}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}