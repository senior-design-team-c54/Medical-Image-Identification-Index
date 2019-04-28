using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MI3.Models
{
    public class AbstractViewModel
    {
        [Required]
        [Display(Name = "URL", Description = "Include the URL from which this dataset can be accessed")]
        [Url]
        public string Url { get; set; }

        /*
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        */

        [Required]
        [Display(Name = "Please select this box if you would like us to host your data on our servers (if not, you must have a URL at which the data can be accessed)")]
        public bool Rehost { get; set; }

        [Required]
        [Display(Name = "Would you like to make your data publicly searchable or available only to registered users?")]
        public bool PublicAccess { get; set; }

        [Required]
        [Display(Name = "Share any additional information about your dataset, e.g., modalities, patient demographics, anatomical regions, etc.")]
        public string Content { get; set; }
    }
}