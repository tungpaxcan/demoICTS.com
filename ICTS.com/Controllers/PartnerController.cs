using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ICTS.com.Models;

namespace ICTS.com.Controllers
{
    public class PartnerController : Controller
    {
        private Entities db = new Entities();
        // GET: Partner
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult Partner() { 
            try
            {
                var pn = (from s in db.Partners.Where(x => x.Status != false)
                          select new
                          {
                              id = s.Id,
                              name = s.Name,
                              title = s.Title,
                              image = s.Image,
                              link = s.Link
                          }).ToList();
                return Json(new { code = 200, pn = pn, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}