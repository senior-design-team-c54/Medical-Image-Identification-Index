using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MI3.Models.Repo
{
    public enum FileType { Dicom,Labels, Folder}

    public class RepoFile 
    {
        public FileType fileType { get; protected set; }
        public RepoFolder Parent { get; set; }


        public string FullPath { get { return name; } }
        public string RelativePath
        {
            get {
                int sIndex = name.IndexOf('/');
                if (sIndex == -1 || sIndex == name.Length - 1) return name;
                return name.Substring(sIndex + 1);

            }
        }
        //The name of this file or folder
        public string name;

        public override string ToString() {
            return name;
        }

    }
}