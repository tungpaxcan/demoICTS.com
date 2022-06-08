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

namespace ICTS.com.Controllers
{
    public class SolutionHomeController : Controller
    {
        private Entities db = new Entities();

        // GET: SolutionHome

        public  ActionResult Index()
        {
            return View();
        }
        public  ActionResult Details(string meta,int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
             DetailSolution solution =  db.DetailSolutions.Find(id);
            if (solution == null)
            {
                return HttpNotFound();
            }
            return View(solution);
        }
        [HttpGet]
        public JsonResult Solution()
        {
            try
            {
                var left = (from s in db.Solutions.Where(x => x.Status != false )
                            select new
                            {
                                id = s.Id,
                                title = s.Title,
                                meta = s.Meta,
                                name = s.Name,
                                content = s.Content,
                                image = s.Image
                            }).ToList();
                return Json(new { code = 200, left = left, msg = "Hiển thị dữ liệu "  }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult DetailSolution(int id)
        {
            try
            {
                var left = (from s in db.DetailSolutions.Where(x => x.Status != false &&x.IdSolution==id)
                            join so in db.Solutions on s.IdSolution equals so.Id
                            select new
                            {
                                id = s.Id,
                                title = so.Title,
                                meta = so.Meta,
                                name = so.Name,
                                content = s.Content,
                                image = s.Image
                            }).ToList();
                return Json(new { code = 200, left = left, Url = "/giai-phap/chi-tiet-giai-phap/" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult DetailSolutionExcept(int id)
        {
            try
            {
                var left = (from s in db.DetailSolutions.Where(x=>x.Status!=false && x.IdSolution!=id)
                             join so in db.Solutions on s.IdSolution equals so.Id
                            select new
                            {
                                id = s.Id,
                                idsolution = s.IdSolution,
                                title = so.Title,
                                meta = so.Meta,
                                name = so.Name,
                                content = s.Content,
                                image = s.Image
                            }).ToList();
                                     
                return Json(new { code = 200, left = left, msg = "Hiểm thị dữ liệu " }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Hiểm thị dữ liệu thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
