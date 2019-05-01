using MI3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core;
using System.Net.Http;
using MI3.Models.FormData;
using System.IO;
using System.Globalization;

namespace MI3.Controllers
{
    [RoutePrefix("Repository")]
    public class RepositoryController : Controller
    {

        // GET: View/0
        [Route("View/{id}")]
        public ActionResult View(string id)
        {
            var collection = Database.DB.DataCollection;
            var doc = collection.Find(x => x.ID == id).FirstOrDefault();
            if(doc == default(Repository)) {
                return RedirectToAction("Index", "Search");
            }

            return View("View", doc);
        }
        public ActionResult Index() {
            return View();
        }

        [HttpGet]
        public ActionResult Upload()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UploadDataSet() {
            return View();
        }



        /// <summary>
        /// Receives the meta info needed to set up an empty repository
        /// </summary>
        [HttpPost]
       // [ValidateAntiForgeryToken]
        public ActionResult ReceiveMeta(UploadMeta meta) {
            var Nvc = Request.Form;
            //create the empty repository starting with the metaData
            HttpContext.Session["Repository"] = new Repository(meta);
            HttpContext.Session["RepoCount"] = 0;
            //return RedirectToAction("Index", "Search");

            return new JsonResult { Data = meta };
        }




        
        /// <summary>
        /// Receives the zip file meta data specifying what info will be passed to the server, so we can setup the structure to receive the files
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ReceiveZipMeta(UploadZipMeta zipMeta) {
            
            //null check
            if (HttpContext.Session["Repository"] == null) {
                return new JsonResult { Data = "failure" };
            }
            //initialize data portion of repo using zip file meta data
            ((Repository)HttpContext.Session["Repository"]).initializeFromZipMeta(zipMeta);

            
            return new JsonResult { Data = "success" };
        }
        [HttpPost]
        public JsonResult ReceiveZipFile(UploadFile file) {
            if (HttpContext.Session["RepoCount"] == null)
                HttpContext.Session["RepoCount"] = 0;

            int count = (int)HttpContext.Session["RepoCount"];

            //null check
            if (HttpContext.Session["Repository"] == null) {
                return new JsonResult { Data = "failure" };
            }
            //TODO: store files in database individually instead of storing all of them in session before storing it.
            //add data file to repo
            ((Repository)HttpContext.Session["Repository"]).addParseFile(file);
            count++;
            HttpContext.Session["RepoCount"] = count;

            //if last file
            if(count == ((Repository)HttpContext.Session["Repository"]).TotalFiles) {
                //store repository now that ti is finished being initialized, hten erase it from session context (to save memory)
                Database.DB.AddRepository(((Repository)HttpContext.Session["Repository"]));
                HttpContext.Session["Repository"] = null;
                //go back to search to show it completed
                HttpContext.Session["RepoCount"] = 0;
                //return View("View",0);
                return Json(new { result = "Redirect", url = Url.Action("Index", "Search") });
            }
            //return View("View",0);

            return new JsonResult { Data = "success" };

        }

        [HttpGet]
        public ActionResult Heartbeat() {

            return View();
        }
        [HttpPost]
        public JsonResult ReceiveHeartbeat() {
            DateTime now = DateTime.Now;
            DateTime inTime;
            string data;
            Dictionary<string, string> json;
            using (StreamReader sr = new StreamReader(HttpContext.Request.InputStream)) {
                data = sr.ReadToEnd();
                json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(data);
                inTime = DateTime.Parse(json["date"]);
            }
            return new JsonResult { Data = (now - inTime).TotalMilliseconds };
        }
    }
}