using System;
using System.IO;
using System.Web.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;

namespace MI3.Tests
{
    [TestClass]
    public class DatabaseTest
    {

        
        [TestMethod]
        public void LoadDB() {
            //cannot succeed because Unit Tests cannot Access App_Data folder
            var db = new MI3.Database();
            // Assert.IsTrue(true);
            Assert.IsTrue(db.client.isDatabaseAvailable());

        }
        [TestMethod]
        public void GetRepos() {
            var db = new MI3.Database();
            var allRepos = db.FindRepo(x => true);
            foreach (var r in allRepos)
            Assert.IsTrue(allRepos.Count > 0);
        }

        
    }
}
