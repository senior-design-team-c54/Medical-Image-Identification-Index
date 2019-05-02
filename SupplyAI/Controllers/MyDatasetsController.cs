using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MI3.Models;
using Microsoft.AspNet.Identity;

namespace MI3.Controllers
{
    public class MyDatasetsController : Controller
    {
        // GET: MyDatasets
        [Authorize]
        public ActionResult Index()
        {
            Database db = new Database();
            List<Repository> usersDatasets = db.FindRepo(doc => doc.MetaData.UserName == User.Identity.GetUserName());
            ViewBag.Title = "My Datasets.";
            return View("~/Views/Search/Index.cshtml", usersDatasets);
        }
    }
}