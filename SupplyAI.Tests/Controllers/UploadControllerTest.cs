using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MI3;
using MI3.Controllers;

namespace SupplyAI.Tests.Controllers
{
    [TestClass]
    public class UploadControllerTest
    {
        [TestMethod]
        public void Index() {
            // Arrange
            UploadController controller = new  UploadController();
            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void SubmitAbstract() {
            // Arrange
            UploadController controller = new UploadController();

            // Act
            ViewResult result = controller.SubmitAbstract() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
                }

     
        
    }
}
