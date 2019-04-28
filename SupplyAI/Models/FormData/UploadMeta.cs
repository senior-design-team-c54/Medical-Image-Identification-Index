using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MI3.Models.FormData
{
    /// <summary>
    /// Contains all meta data needed for creating a new repository (besides what is initialized server side). Includes Name, description, ...
    /// </summary>
    public class UploadMeta
    {
        public string[] authors { get; set; }
        public string availability { get; set; }
        public string url { get; set; }
        public string peer_reviewed { get; set; }


        // public int NumFiles => files.Length;
        // public int NumFolders => folders.Length;


        [Required]
        public int totalSize { get; set; } //total size of uploaded data in bytes
        public string name { get; set; }
        public string summary { get; set; }

        

    }
}