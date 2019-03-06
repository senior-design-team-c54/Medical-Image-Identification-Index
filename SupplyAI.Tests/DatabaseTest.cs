using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;

namespace SupplyAI.Tests
{
    [TestClass]
    public class DatabaseTest
    {
        
        [TestMethod]
        public void RelativeFileAccess() {
            var file = @"App_Data/login.pass";
            Assert.IsTrue(File.Exists(file));
        }
        [TestMethod]
        public void LoadDB() {

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
