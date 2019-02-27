using SupplyAI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core;

namespace SupplyAI.Controllers
{
    [RoutePrefix("Repository")]
    public class RepositoryController : Controller
    {
        // GET: View/0
        [Route("View/{id}")]
        public ActionResult View(ulong id)
        {
            var collection = Startup.database.DataCollection;
            var doc = collection.Find(x => x.ID == id).FirstOrDefault();
            if(doc == default(Repository)) {
                return RedirectToAction("Index", "Search");
            }
            return View("View", doc);
        }
        public ActionResult Index() {

            return View();
        }
    }
}