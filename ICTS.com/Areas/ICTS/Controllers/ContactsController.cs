using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ICTS.com.Models;
namespace ICTS.com.Areas.ICTS.Controllers
{
    [HandleError]
    public class ContactsController : BaseController
    {
        private Entities db = new Entities();
        // GET: ICTS/Contacts
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult ContactStatus()
        {
            try
            {
                var ct = (from tt in db.Contacts.Where(x => x.Status == null)
                         
                          select new
                          {
                              id = tt.Id,
                              name = tt.Name,
                              idproduct = tt.IdProduct,
                              phone =tt.Phone,
                              email=tt.Email,
                              enterprise = tt.NameEnterprise,
                              amount = tt.Amount,
                              status=tt.Status,
                              createdate = tt.CreateDate.Value.Day + "/" + tt.CreateDate.Value.Month + "/" + tt.CreateDate.Value.Year,
                              modifiledate = tt.ModifileDate.Value.Day + "/" + tt.ModifileDate.Value.Month + "/" + tt.ModifileDate.Value.Year,
                          }).ToList();
                var result = ct.Count();
                var session = (Admin)Session["admin"];
                var role = session.Role;
                return Json(new { code = 200,role=role ,result = result, ct = ct, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult Contact(string seach,int page)
        {
            try
            {
                var pageSize = 5;
                var dmspp = (from tt in db.Contacts.Where(x => x.Id > 0||x.IdProduct==null)
                             join s in db.Products on tt.IdProduct equals s.Id
                             orderby tt.CreateDate descending
                             select new
                          {
                              id = tt.Id,
                              name = tt.Name,
                              nameproduct = s.Name,
                              idproduct = tt.IdProduct,
                              phone = tt.Phone,
                              email = tt.Email,
                              enterprise = tt.NameEnterprise,
                              amount = tt.Amount,
                              status = tt.Status ,
                              createdate = tt.CreateDate.Value.Day + "/" + tt.CreateDate.Value.Month + "/" + tt.CreateDate.Value.Year,
                              modifiledate = tt.ModifileDate.Value.Day + "/" + tt.ModifileDate.Value.Month + "/" + tt.ModifileDate.Value.Year,
                          }).ToList().Where(x=>x.name.ToLower().Contains(seach)|| x.nameproduct.ToLower().Contains(seach)||
                                              x.phone.ToLower().Contains(seach)||x.email.ToLower().Contains(seach)||
                                              x.enterprise.ToLower().Contains(seach)||x.amount.ToString().ToLower().Contains(seach));
                var pages = dmspp.Count() % pageSize == 0 ? dmspp.Count() / pageSize : dmspp.Count() / pageSize + 1;
                var ct = dmspp.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                return Json(new { code = 200, pages=pages, ct = ct, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult Detail(int id)
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                var tv = db.Contacts.SingleOrDefault(x => x.Id == id);
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
                var l = db.Contacts.Find(id);
                db.Contacts.Remove(l);
                db.SaveChanges();
                return Json(new { code = 200, msg = "Xoa Dữ Liệu thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Xoa nhật dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult Edit(int id)
        {
            try
            {
                var dmsp = db.Contacts.SingleOrDefault(x => x.Id == id);
                dmsp.Status = false;
                db.SaveChanges();
                return Json(new { code = 200, msg = "Sửa danh sách thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, mmsg = "Sửa danh sách không thành công" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult ListGmail()
        {
            try
            {
                var ct = (from tt in db.Contacts.Where(x => x.Id>0)
                          select new
                          {
                              id = tt.Id,
                              email = tt.Email
                          }).ToList();

                return Json(new { code = 200, ct = ct, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}