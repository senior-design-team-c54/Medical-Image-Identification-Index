using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace SupplyAI.Models
{
    public class Series
    {
        public List<Study> studies;
        //List<Tag> tags;

        public Series(string directory) {
            readInSeries(directory);
        }
        public void readInSeries(string directory) {
            studies = new List<Study>();
            string[] folders = Directory.GetDirectories(directory);

            int index = 1;
            foreach (string folder in folders) {
                Console.WriteLine("Reading Study: {0}", index++);
                studies.Add(new Study(folder, true));
                Console.WriteLine("Done");
                // Console.Read();
            }
            //add study of all dicom files in this directory
            Console.WriteLine("Reading Study: {0}", index++);
            studies.Add(new Study(directory, false));

        }


    }
}