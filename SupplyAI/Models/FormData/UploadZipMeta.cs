using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MI3.Models.FormData
{
    public class UploadZipMeta
    {


        public string[] files { get; set; } //list of all files being uploaded

        [Required]
        public int totalSize { get; set; } //total size of uploaded data in bytes
        public int totalFiles { get; set; } //total number of dicom files to load

    }
}