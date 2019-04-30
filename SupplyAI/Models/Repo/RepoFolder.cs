using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq;
using Dicom;

namespace MI3.Models.Repo
{
    public class RepoFolder : RepoFile
    {

        public List<DicomFile> DicomFiles => _items.Where(f => f.fileType == FileType.Dicom).Select(f => ((RepoDicomFile)f).dicomFile).ToList();
        public List<RepoFile> Files { get { return _items.Where(f => f.fileType != FileType.Folder).ToList(); } }
        public List<RepoFolder> Folders { get { return _items.Where(f => f is RepoFolder).Cast<RepoFolder>().ToList(); } }

        public List<RepoFile> Items { get { return _items; } set { _items = value; } }

        /// <summary>
        /// absolute folder name
        /// </summary>
        
        /// <summary>
        /// All folders and files childed to this object
        /// </summary>
        public List<RepoFile> _items;


        public RepoFolder(string name) {
            this.name = name;
            fileType = FileType.Folder;
            _items = new List<RepoFile>();
        }
        public bool addFile(RepoFile item) {
            if (item == null || Items.Any(it => it.name == item.name)) return false; //dont add duplicates

            if (!item.name.Contains(name)) return false; //item shouldn't have moves into this folder, because name doesnt match
            //relative pathing name from this folder
            string relName = item.name.Substring(name.Length);        
            int nextSlash = relName.IndexOf('/');            
            //if relName has no more '/' then it is in correct directory to save
            if (nextSlash == -1 || nextSlash == relName.Length - 1) {
                //save here
                Items.Add(item);
                return true;
            }
            //needs to go to subdirectory, find which one
            string nextSubdir = relName.Substring(0, nextSlash + 1);
            RepoFolder follow = Folders.FirstOrDefault(f => f.RelativePath == nextSubdir);
            if (follow == null) { //cant trust order of file tree structure, just create directory if it doesnt exist yet and instead ignore duplicates
                follow = new RepoFolder(name+nextSubdir); 
                //must not exist so no more error checking required
                Items.Add(follow);
            }
            follow.addFile(item);
            return true;

        }
        
        public bool containsTag(Dicom.DicomTag tag) {
            foreach(var dicom in DicomFiles) {
                if (dicom.Dataset.Contains(tag)) return true;
            }
            return false;
        }

    }
}