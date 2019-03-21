using SupplyAI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace SupplyAI.Controllers
{
    public class HomeController : Controller
    {


        public HomeController() {
        }


        public ActionResult IsMongoDBLive() {
            ViewBag.isLive = System.IO.File.Exists(Server.MapPath( "~/bin/App_Data/login.pass")); 
            return View();
        }

        public ActionResult Info(int ID) {

            return View();
        }

        public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}