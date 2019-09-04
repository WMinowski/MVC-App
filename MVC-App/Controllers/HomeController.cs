using MVC_App.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_App.Controllers
{
    public class HomeController : Controller
    {
        public readonly testEntities db = new testEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult RelationView()
        {
            //using (testEntities db = new testEntities())
            //{
            //    //tblRelation relation = db.tblRelation.Where(p => Int32.Parse(p.Id.ToString()) == id).FirstOrDefault();
            //    return View(db.tblRelation);
            //}
            return View(db.tblRelation);
        }

        //protected override void Dispose(bool disposing)
        //{
        //    db.Dispose();
        //    base.Dispose(disposing);
        //}
    }
}