using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MI3.Models.FormData;
using Dicom;
using System.IO;
using System.Text;

namespace MI3.Models.Repo
{


    public class RepoDicomFile : RepoFile
    {
        public Dicom.DicomFile dicomFile;
        

        public RepoDicomFile(UploadFile file) {
            this.name = file.name;
            //file.data += 
            fileType = FileType.Dicom;

            //current conversion is: js UInt8Array -> js base64String -> c# string -> c# byte[] -> c# Stream
            //If a more direct conversion from js UInt8Array -> C# Stream is found, it should be used instead

            using(var ms = new MemoryStream(Encoding.Default.GetBytes(file.data))) {
                dicomFile = DicomFile.Open(ms);
            }
            
        }
    }
}