using Dicom;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MI3.Models
{
    public class Study
    {
        public List<DicomFile> dicomFiles;

        /// <summary>
        /// Read study from directory
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="recursive"></param>
        public Study(string directory, bool recursive = false) {
            SearchOption op = recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            dicomFiles = new List<DicomFile>();
            string[] files = Directory.GetFiles(directory, "*.*", op);

            foreach (string file in files) {
                if (true) {
                    dicomFiles.Add(DicomFile.Open(file));
                }
            }


        }

    }
}