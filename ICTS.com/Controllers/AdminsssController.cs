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
                var date = DateTime.Now;
                return Json(new { code = 200, date = date, d = d, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}