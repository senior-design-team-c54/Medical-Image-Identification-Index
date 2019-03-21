using System;
using System.IO;
using System.Web.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;

namespace SupplyAI.Tests
{
    [TestClass]
    public class DatabaseTest
    {

        
        [TestMethod]
        public void LoadDB() {
            //cannot succeed because Unit Tests cannot Access App_Data folder
            var db = new SupplyAI.Database();
            // Assert.IsTrue(true);
            Assert.IsTrue(db.client.isDatabaseAvailable());

        }
        [TestMethod]
        public void GetRepos() {
            var db = new SupplyAI.Database();
            var allRepos = db.FindRepo(x => true);
            foreach (var r in allRepos)
            Assert.IsTrue(allRepos.Count > 0);
        }

        
    }
}
