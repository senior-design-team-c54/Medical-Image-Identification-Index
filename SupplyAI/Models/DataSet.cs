using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SupplyAI.Models
{
    public class DataSet
    {


        public readonly UInt64 ID; //the Unique ID for each DataSet, no duplicates allowed. Cannot be changed after initialization

        public string Name; 
        public Dictionary<string,Tag> Tags = new Dictionary<string, Tag>(); // never null so no null check issues
        public string Summary = "<NO SUMMARY AVAILABLE>";
        public List<String> Authors = new List<string>();

        public DataSet() {
            ID = GenerateUniqueID();
        }
        private void addTag(Tag t) {
            //dont allow duplicates
            if (Tags.ContainsValue(t)) return;
            Tags[t.Name] = t;

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