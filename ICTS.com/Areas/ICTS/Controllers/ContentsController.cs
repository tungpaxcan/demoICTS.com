using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ICTS.com.Models;

namespace ICTS.com.Areas.ICTS.Controllers
{
    public class ContentsController : Controller
    {
        private Entities db = new Entities();
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
                    _FileName = sub11 + sub21 + "PartnerContent" + "." + file.FileName.Substring(index + 1);
                    file.SaveAs(Server.MapPath("/Images/" + _FileName));

                    return "/Images/" + _FileName;
                }

            }
            return "";

        }
        // GET: ICTS/Contents
        [HttpGet]
        public JsonResult PartnerContent()
        {
            try
            {
                var ct = (from tt in db.Modules.Where(x => x.Id == 19)
                          select new
                          {
                              id = tt.Id,
                              dec = tt.Description,
                              name = tt.Name,
                              meta = tt.Meta,
                              title = tt.Title,
                              image = tt.Image,
                              image1=tt.Image1,
                              image2=tt.Image2,
                              image3=tt.Image3,
                              createdate = tt.CreateDate.Value.Day + "/" + tt.CreateDate.Value.Month + "/" + tt.CreateDate.Value.Year,
                              createby = tt.CreateBy,
                              modifiledate = tt.ModifileDate.Value.Day + "/" + tt.ModifileDate.Value.Month + "/" + tt.ModifileDate.Value.Year,
                              modifileby = tt.ModifileBy
                          }).ToList();
                return Json(new { code = 200, ct = ct, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult DetailPartnerContent(int id)
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
        [HttpPost,ValidateInput(false)]
        public JsonResult EditPartnerContent(int id, string name, string content)
        {
            try
            {
                var session = (Admin)Session["admin"];
                var nameAdmin = session.Name;
                var why = db.Modules.SingleOrDefault(x => x.Id == id);
                why.ModifileBy = nameAdmin;
                why.ModifileDate = DateTime.Now;
                why.Name = name;
                why.Content = content;
                db.SaveChanges();
                return Json(new { code = 200, msg = "Sửa danh sách thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, mmsg = "Sửa danh sách không thành công" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}