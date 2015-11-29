using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SkyBillViewer;
using SkyBillViewer.Controllers;
using SkyBillViewer.Models;
using SkyBillViewer.Tests.Controllers;

namespace SkyBillViewer.Tests.Controllers
{
    [TestClass]
    public class BillAPIControllerTest
    {
        private string _url = "http://safe-plains-5453.herokuapp.com/bill.json";

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),"The path is not of a legal form.")]
        public void API_BillAPI_GetBillData_ExpectReturnPathIllegalExpection()
        {
            // Arrange
            BillAPIController controller = new BillAPIController();
            // Act
            var result = controller.GetBillData(string.Empty) as BillModel;
            // Assert
        }

        [TestMethod]
        public void API_BillAPI_GetBillData_ExpectModelRetured()
        {
            // Arrange
            BillAPIController controller = new BillAPIController();

            // Act
            var result = controller.GetBillData(_url) as BillModel;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void API_BillAPI_GetBillData_ExpectJsonDataReturned()
        {
            // Arrange
            BillAPIController controller = new BillAPIController();

            // Act
            var result = controller.GetBillData(_url) as BillModel;

            // Assert
            Assert.IsNotNull(result.CallCharge);
            Assert.IsNotNull(result.Package);
            Assert.IsNotNull(result.SkyStore);
            Assert.IsNotNull(result.Statement);
            Assert.AreEqual((decimal)136.03, result.Total);
        }
        

    }
}
