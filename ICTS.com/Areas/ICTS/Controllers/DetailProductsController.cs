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
    public class DetailProductsController : Controller
    {
        private Entities db = new Entities();
        [HttpGet]
        public JsonResult Id()
        {
            try
            {
                var dmsp = (from s in db.DetailProducts.Where(x => x.Id>0)
                            select new
                            {
                                idproduct = s.IdProduct
                            }).ToList();
                return Json(new { code = 200, dmsp = dmsp, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost, ValidateInput(false)]
        public JsonResult Edit(int id,string application,string dec,string dectech,string option)
        {
            try
            {
                var session = (Admin)Session["admin"];
                var nameAdmin = session.Name;
                var a = db.DetailProducts.SingleOrDefault(x=>x.IdProduct==id);
                    a.C_content_Application = application;
                    a.C_content_Dec = dec;
                    a.C_content_DecTech = dectech;
                    a.C_content_Option = option;
                    a.CreateBy = nameAdmin;
                    a.CreateDate = DateTime.Now;
                    a.ModifileBy = nameAdmin;
                    a.ModifileDate = DateTime.Now;
                    db.SaveChanges();

                return Json(new { code = 200, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost, ValidateInput(false)]
        public JsonResult Add(int id, string application, string dec, string dectech, string option)
        {
            try
            {
                var session = (Admin)Session["admin"];
                var nameAdmin = session.Name;
                var d = new DetailProduct();
                d.IdProduct = id;
                d.C_content_Application = application;
                d.C_content_Dec = dec;
                d.C_content_DecTech = dectech;
                d.C_content_Option = option;
                d.CreateBy = nameAdmin;
                d.CreateDate = DateTime.Now;
                d.ModifileBy = nameAdmin;
                d.ModifileDate = DateTime.Now;
                db.DetailProducts.Add(d);
                db.SaveChanges();

                return Json(new { code = 200, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
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
               var tv = db.DetailProducts.SingleOrDefault(x => x.IdProduct == id);
                return Json(new { code = 200, tv = tv, msg = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
