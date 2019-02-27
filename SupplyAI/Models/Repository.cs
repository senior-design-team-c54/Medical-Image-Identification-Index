using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson.Serialization.Attributes;

namespace SupplyAI.Models
{
    
    public class Repository
    {
        //Properties: These are read using reflection to display on the Repository.cshtml page. Underscores are replaced with spaces
        //These must also be strings since their only goal is to be printed (This might be changed at a later date?)
        [BsonElement("name")]
        public string Name { get; private set; }
        [BsonElement("summary")]
        public string Summary { get; set; }
        //we dont print authors normally, since they get clickable bubbles
        [BsonElement("authors")]
        public List<string> Authors { get; set; } = new List<string>();
        [BsonElement("source")]
        public string Source { get; set; }
        [BsonElement("availability")]
        public string Availability { get; set; }
        [BsonElement("url")]
        public string Url { get; set; }
        [BsonElement("peer_review")]
        public string Peer_Reviewed { get; set; }
        [BsonElement("data_size")]
        public long Data_Size { get; set; }
        //we dont print Tags normally, since they get clickable bubbles
        [BsonElement("tags")]
        public Dictionary<string, Tag> Tags { get; private set; } = new Dictionary<string, Tag>();
        [BsonElement("pub_type")]
        public string Publication_Type { get; set; }
        [BsonElement("label_type")]
        public string Label_Type { get; set; }
        [BsonElement("pub_date")]
        public DateTime Publication_Date { get; set; }
        [BsonElement("refs")]
        public List<Reference> References { get; set; }
        [BsonId]
        public UInt64 ID { get; private set; }

        

        public Repository(string name = "None") {
            ID = GenerateUniqueID();
            Name = name;
        }

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
        public static UInt64 GenerateUniqueID() {
            return _nextID++;
        }

        public string listAuthors() {
            string builder = "";
            
            for(int i =0; i <Authors.Count; i++) {
                builder += Authors[i];
                if (i != Authors.Count)
                    builder += ", ";
            }
            return builder;
        }

    }
}