using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MI3.Models
{
    public class ManageUsersViewModel
    {
        // Key is UserName (email) or Id, value is new role
        public Dictionary<string, string> UpdatedUserRoles { get; set; }
    }
}