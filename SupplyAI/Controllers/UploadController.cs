using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MI3.Models;


namespace MI3.Controllers
{
    public class UploadController : Controller
    {
        // GET: Upload
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        /*
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Instructions()
        {
            return View("Abstract")
        }
        */
        [HttpGet]
        [Authorize]
        public ActionResult SubmitAbstract()
        {
            return View("Abstract");
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitAbstract(AbstractViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Abstract newAbstract = new Abstract(User.Identity.GetUserName(), model.Url, model.Rehost, model.PublicAccess, model.Content);
            var notification = newAbstract.GenerateNewAbstractNotification();

            newAbstract.AddToDb();
            notification.AddToDb();

            return View("~/Views/Home/Index.cshtml");          
        }
    }
}