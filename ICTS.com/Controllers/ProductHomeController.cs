using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ICTS.com.Models;

namespace ICTS.com.Controllers
{
    [HandleError]
    public class ProductHomeController : Controller
    {
        private Entities db = new Entities();
        // GET: ProductHome
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult ProductImageById(int id,int page) 
        {
            try
            {
                var pageSize = 9;
                var dmspp = (from s in db.Products.Where(x => x.Status != false && x.IdProductCategory == id)
                            join ss in db.ProductCategories on s.IdProductCategory equals ss.Id
                            join sss in db.Brands on s.IdBrand equals sss.Id
                            join ssss in db.Categories on ss.IdCategory equals ssss.Id
                            select new
                            {
                                id= s.Id,
                                namecategory =ssss.Name,
                                metacategory =ssss.Meta,
                                nameproductcategory =ss.Name,
                                metaproductcategory =ss.Meta,
                                namebrand =sss.Name,
                                metabrand =sss.Meta,
                                meta = s.Meta,
                                name = s.Name,
                                title=s.Title,
                                content = s.Content,
                                image = s.Image

                            }).ToList();
                var pages = dmspp.Count() % pageSize == 0 ? dmspp.Count() / pageSize : dmspp.Count() / pageSize + 1;
                var dmsp = dmspp.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                return Json(new { code = 200,pages=pages, dmsp = dmsp, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult ProductOrder(int id, int page)
        {
            try
            {
                var pageSize = 3;
                var dmspp = (from s in db.Products.Where(x => x.Status != false && x.IdProductCategory == id)
                             join ss in db.ProductCategories on s.IdProductCategory equals ss.Id
                             join sss in db.Brands on s.IdBrand equals sss.Id
                             join ssss in db.Categories on ss.IdCategory equals ssss.Id
                             select new
                             {
                                 id = s.Id,
                                 idprocate=s.IdProductCategory,
                                 meta = s.Meta,
                                 name = s.Name,
                                 content = s.Content,
                                 image = s.Image

                             }).ToList();
                var pages = dmspp.Count() % pageSize == 0 ? dmspp.Count() / pageSize : dmspp.Count() / pageSize + 1;
                var dmsp = dmspp.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                return Json(new { code = 200, pages = pages, dmsp = dmsp, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult ProductImageBy()
        {
            try
            {

                var dmsp = (from s in db.Products.Where(x => x.Status != false )
                            join ss in db.ProductCategories on s.IdProductCategory equals ss.Id
                            join sss in db.Brands on s.IdBrand equals sss.Id
                            join ssss in db.Categories on ss.IdCategory equals ssss.Id
                            select new
                            {
                                namecategory = ssss.Name,
                                metacategory = ssss.Meta,
                                nameproductcategory = ss.Name,
                                metaproductcategory = ss.Meta,
                                namebrand = sss.Name,
                                metabrand = sss.Meta,
                                meta = s.Meta,
                                name = s.Name,
                                content = s.Content,
                                image = s.Image

                            }).ToList();
                return Json(new { code = 200, dmsp = dmsp, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult Seach(int page, string seach)
        {
            try
            {
                var pageSize = 9;
                var dmspp = (from s in db.Products.Where(x => x.Status != false )
                            join ss in db.ProductCategories on s.IdProductCategory equals ss.Id
                            join sss in db.Brands on s.IdBrand equals sss.Id
                            join ssss in db.Categories on ss.IdCategory equals ssss.Id
                            select new
                            {
                                id=s.Id,
                                namecategory = ssss.Name,
                                metacategory = ssss.Meta,
                                nameproductcategory = ss.Name,
                                metaproductcategory = ss.Meta,
                                namebrand = sss.Name,
                                metabrand = sss.Meta,
                                title = s.Title,
                                meta = s.Meta,
                                name = s.Name,
                                content = s.Content,
                                image = s.Image

                            }).ToList().Where(x => x.namecategory.ToLower().Contains(seach) || x.metacategory.ToLower().Contains(seach) ||
                                                x.nameproductcategory.ToLower().Contains(seach) || x.metaproductcategory.ToLower().Contains(seach) ||
                                                x.namebrand.ToLower().Contains(seach) || x.metabrand.ToLower().Contains(seach) ||
                                                x.name.ToLower().Contains(seach) || x.meta.ToLower().Contains(seach) ||
                                                x.content.ToLower().Contains(seach));
                var pages = dmspp.Count() % pageSize == 0 ? dmspp.Count() / pageSize : dmspp.Count() / pageSize + 1;
                var dmsp = dmspp.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                return Json(new { code = 200, pages=pages,dmsp = dmsp, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Details(string meta, int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetailProduct derpo = db.DetailProducts.Find(id);
            if (derpo == null)
            {
                return HttpNotFound();
            }
            return View(derpo);
        }
        [HttpGet]
        public JsonResult DetailProductById(int id)
        {
            try
            {
                var left = (from s in db.DetailProducts.Where(x => x.Status != false && x.IdProduct == id)
                            join so in db.Products on s.IdProduct equals so.Id
                            join soo in db.Brands on so.IdBrand equals soo.Id
                            select new
                            {
                                id = so.Id,
                                ids=s.Id,
                                idprocate=so.IdProductCategory,
                                title = so.Title,
                                meta = so.Meta.Replace("/",""),
                                name = so.Name,
                                brand = soo.Name,
                                application = s.C_content_Application,
                                dec = s.C_content_Dec,
                                dectech = s.C_content_DecTech,
                                option = s.C_content_Option,
                                image = so.Image
                            }).ToList();
                return Json(new { code = 200, left = left, Url = "/chi-tiet-san-pham/" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult ProductLook(string seach,int page)
        {
            try
            {
                var pageSize = 3;
                var dmspp = (from s in db.Products.Where(x => x.Status != false && x.Status != false)
                             select new
                             {
                                 id = s.Id,
                                 meta = s.Meta,
                                 name = s.Name,
                                 content = s.Content,
                                 image = s.Image
                             }).ToList().Where(x=>x.name.ToLower().Contains(seach)||x.meta.ToLower().Contains(seach));
                var pages = dmspp.Count() % pageSize == 0 ? dmspp.Count() / pageSize : dmspp.Count() / pageSize + 1;
                var dmsp = dmspp.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                return Json(new { code = 200,pages=pages, dmsp = dmsp, msg = "Hiển Thị Dữ liệu thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}