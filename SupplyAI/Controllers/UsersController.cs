using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MI3.Models;
using MongoDB.AspNet.Identity;

namespace MI3.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Manage()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Manage(ManageUsersViewModel model)
        {
            ApplicationUserManager applicationUserManager = new ApplicationUserManager(new UserStore<ApplicationUser>());
            foreach (KeyValuePair<string, string> userAndRole in model.UpdatedUserRoles)
            {
                if (userAndRole.Value != "")
                {
                    IdentityUser user = applicationUserManager.FindByNameAsync(userAndRole.Key).Result;
                    if (user.Roles.Contains("admin") && userAndRole.Value == "user")
                    {
                        applicationUserManager.RemoveFromRoleAsync(user.Id, "admin");
                    }
                    else
                    {
                        applicationUserManager.AddToRoleAsync(user.Id, userAndRole.Value);
                    }
                }
            }
            return View();
        }
    }
}