using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SkyBillViewer;
using SkyBillViewer.Controllers;
using SkyBillViewer.Models;

namespace SkyBillViewer.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        private HomeController _controller;

        [TestMethod]
        public void MVC_Index_ViewResult_ViewExists()
        {
            // Arrange
            _controller = new HomeController();
            string expectedView = "Index";

            // Act
            var actual = _controller.Index() as ViewResult;
            // Assert
            Assert.AreEqual(expectedView, actual.ViewName);
        }

        [TestMethod]
        public void MVC_Index_ViewResult_ModelReturned()
        {
            // Arrange
            _controller = new HomeController();
            string expectedView = "Index";

            // Act
            var actual = _controller.Index() as ViewResult;
            var actualModel = actual.Model as BillModel;

            // Assert
            Assert.IsNotNull(actualModel);
            Assert.AreEqual(expectedView, actual.ViewName);
        }

        [TestMethod]
        public void MVC_Index_AngularJS_Json_ViewResult_ViewExists()
        {
            // Arrange
            _controller = new HomeController();
            string expectedView = "Index_AngularJS";
            // Act
            var actual = _controller.Index_AngularJS() as ViewResult;

            // Assert
            Assert.AreEqual(expectedView, actual.ViewName);
        }

        [TestMethod]
        public void MVC_Index_AngularJS_Json_ViewResult_ModelReturnedNull()
        {
            // Arrange
            _controller = new HomeController();
            // Act
            var actual = _controller.Index_AngularJS() as ViewResult;

            // Assert
            Assert.IsNull(actual.Model);
        }
    }
}
