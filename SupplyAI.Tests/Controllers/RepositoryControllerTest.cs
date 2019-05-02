using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MI3;
using MI3.Controllers;
using MI3.Models.FormData;

namespace SupplyAI.Tests.Controllers
{
    [TestClass]
    public class RepositoryControllerTest
    {
        [TestMethod]
        public void View() {
            // Arrange
            RepositoryController controller = new RepositoryController();
            // Act
            ViewResult result = controller.View(null) as ViewResult;

            // Assert
            Assert.IsNull(result); //should fail
            // Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Index() {
            // Arrange
            RepositoryController controller = new RepositoryController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void UploadDataSet() {
            // Arrange
            RepositoryController controller = new RepositoryController();

            // Act
            ViewResult result = controller.UploadDataSet() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
        

       
        [TestMethod]
        public void ReceiveMeta() {
            // Arrange
            RepositoryController controller = new RepositoryController();
            UploadMeta meta = new UploadMeta();
            // Act
            JsonResult result = controller.ReceiveMeta(meta) as JsonResult;

            Assert.IsTrue(result.Data == meta);
        }

        //Tests fail when interacting with HttpContext
        //[TestMethod]
        //public void ReceiveZipMeta() {
        //    // Arrange
        //    RepositoryController controller = new RepositoryController();
        //    UploadZipMeta meta = new UploadZipMeta();
        //    // Act
        //    JsonResult result = controller.ReceiveZipMeta(meta) as JsonResult;

        //    Assert.IsTrue((string)result.Data == "success");
        //}
        //[TestMethod]
        //public void ReceiveZipFile() {
        //    // Arrange
        //    RepositoryController controller = new RepositoryController();
        //    UploadFile file = new UploadFile();

        //    //Act
        //    JsonResult result = controller.ReceiveZipFile(file);

        //    //no session data should exist, should return "failure"
        //    Assert.IsTrue((string)result.Data == "failure");

        //}

    }
}
