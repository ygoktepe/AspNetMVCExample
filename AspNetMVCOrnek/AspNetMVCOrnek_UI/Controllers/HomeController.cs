using AspNetMVCOrnek_BusinessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspNetMVCOrnek_UI.Controllers
{
    public class HomeController : Controller
    {
        StudentRepo sRepo = new StudentRepo(); // bir classsın icinde baska bir klasıı newleyince bagımlılık olusur
        public ActionResult Index()
        {
            var list = sRepo.GetAll();
            return View(list); //sayfaya model gonderdım
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
    }
}