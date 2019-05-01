using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Dicom;
using MI3.Models.FormData;
using MI3.Models.Repo;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MI3.Models
{
    
    public class Repository
    {
        //Properties: These are read using reflection to display on the Repository.cshtml page. Underscores are replaced with spaces
        //These must also be strings since their only goal is to be printed (This might be changed at a later date?)
        public string Name { get; set; }
        public string Summary { get; set; }
        //we dont print authors normally, since they get clickable bubbles
        public string Authors  { get; set; }
        // public string Source { get; set; }
        public bool Availability =false;// { get => MetaData.PublicAccess; set => MetaData.PublicAccess = value; }
        public string Url { get; set; }
                               //public string Peer_Reviewed => MetaData.
                               //  public long Data_Size { get; set; }
                               //we dont print Tags normally, since they get clickable bubbles
        public Dictionary<string, Tag> Tags { get; private set; } = new Dictionary<string, Tag>();
        //  public string Publication_Type 
        public string Label_Type { get; set; }
        public DateTime Publication_Date = DateTime.Now;//{ get => MetaData.DateGenerated; set => MetaData.DateGenerated = value; }
        //public List<Reference> References { get; set; }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID { get; set; }
        public int TotalFiles { get; set; }

        //public List<DicomFile> files = new List<DicomFile>();
        public Abstract MetaData { get; set; }
        

        


        public Repository(string name = "None") {
            //ID = GenerateUniqueID() ;
            MetaData = new Abstract("DEFAULT NAME", "no url", true, true, "NO SUMMARY FOUND");
            //root = new RepoFolder("");
        }

        public Repository(Abstract data) {
           // root = new RepoFolder("");
            MetaData = data;
            Name = data.DatasetTitle;
            Summary = data.Summary;
            Authors = data.Authors;
            Url = data.Url;
            ID = ObjectId.GenerateNewId().ToString();
            data.DatasetId = ID;
            data.UpdateInDb();

        }

        public void initializeFromZipMeta(UploadZipMeta zipMeta) {
            //root = new RepoFolder("");
            TotalFiles = zipMeta.totalFiles;
            RepoFolder add = null;
            foreach(var file in zipMeta.files) {
                if (file.EndsWith("/")) //is folder
                    add = new RepoFolder(file);
                //for now, only add folders, because we don't know if files are Dicom or labels or other
                //root.addFile(add);
            }

        }
        public bool addParseFile(UploadFile file) {
            if (file == null) return false;
            //now we figure out the file type and add it to the repo
           // RepoFile addFile = null;
            if(file.type == Enum.GetName(typeof(FileType), FileType.Dicom)) {
                addDicomFile(file);
                //is dicom file
                return true;
            }
            if(file.type == Enum.GetName(typeof(FileType), FileType.Labels)) {
               // addFile = new RepoLabelsFile(file);
                return true;
            }

            return false;
        }
        public Dicom.DicomFile addDicomFile(UploadFile file) {
            var name = file.name;
            DicomFile df = null;
            using (var ms = new MemoryStream(Encoding.Default.GetBytes(file.data))) {
                df  = DicomFile.Open(ms);
            }
           // if(df != null)
             //   files.Add(df);
            return df;
        }


        //public bool hasDicomTag(Dicom.DicomTag tag) {
        //    if(tag == null) return false;
        //    return root.containsTag(tag);
        //}



        private void addTag(Tag t) {
            //dont allow duplicates
            if (Tags.ContainsValue(t)) return;
            Tags[t.Name] = t;
                 
        }
        private string sizeNotation(long size) {
            double len = size;
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1) {
                order++;
                len = len / 1024;
            }
            size = (long)(len * 100); //show 2 decimal places
            len = ((double)size) / 100;
            return "" + len + " " + sizes[order];
        }
        
        public void addTags(params string[] tagStrings) {
            var tagList = Startup.Tags.Where(t => tagStrings.Contains(t.Name)); //returns all Tags with strings in the array 'Tags'
            foreach(var t in tagList) {
                addTag(t); //does any error checking for individual tags
            }
        }

        private static UInt64 _nextID = 0;
        private Abstract reviewedAbstract;

        public static UInt64 GenerateUniqueID() {
            return _nextID++;
        }

        //public string listAuthors() {
        //    string builder = "";
            
        //    for(int i =0; i <Authors.Count; i++) {
        //        builder += Authors[i];
        //        if (i != Authors.Count)
        //            builder += ", ";
        //    }
        //    return builder;
        //}

    }
}