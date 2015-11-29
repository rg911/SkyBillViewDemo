using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SkyBillViewer.Models;
using SkyBillViewer.Tests.Models;

namespace SkyBillViewer.Tests.Models
{
    [TestClass]
    public class ModelTest
    {
        
        [TestMethod]
        public void Model_Subbscription_Valid_Exists()
        {
            var model = new SubscriptionsModel();
            Assert.IsNotNull(model);
        }

        [TestMethod]
        public void Model_Subbscription_Valid_ExpectNameRquiredErrors()
        {
            var model = new SubscriptionsModel
            {
                Type = "TV"
            };

            var results = TestModelHelper.Validate(model);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("The Name field is required.", results[0].ErrorMessage);
        }

        [TestMethod]
        public void Model_Subbscription_Valid_ExpectTypeRquiredErrors()
        {
            var model = new SubscriptionsModel
            {
                Name = "Temp"
            };

            var results = TestModelHelper.Validate(model);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("The Type field is required.", results[0].ErrorMessage);
        }

        [TestMethod]
        public void Model_Pakcage_Valid_Exists()
        {
            var model = new PackageModel();
            Assert.IsNotNull(model);
        }

        [TestMethod]
        public void Model_StoreRental_Valid_Exists()
        {
            var model = new StoreRentalsModel();
            Assert.IsNotNull(model);
        }

        [TestMethod]
        public void Model_StoreRentals_Valid_ExpectTypeRquiredErrors()
        {
            var model = new StoreRentalsModel
            {
                Cost = 0
            };

            var results = TestModelHelper.Validate(model);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("The Title field is required.", results[0].ErrorMessage);
        }

        [TestMethod]
        public void Model_StoreBuyAndKeep_Valid_Exists()
        {
            var model = new StoreBuyAndKeepModel();
            Assert.IsNotNull(model);
        }

        [TestMethod]
        public void Model_StoreBuyAndKeep_Valid_ExpectTypeRquiredErrors()
        {
            var model = new StoreBuyAndKeepModel
            {
                Cost = 0
            };

            var results = TestModelHelper.Validate(model);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("The Title field is required.", results[0].ErrorMessage);
        }

        [TestMethod]
        public void Model_SkyStore_Valid_Exists()
        {
            var model = new SkyStoresModel();
            Assert.IsNotNull(model);
        }

        [TestMethod]
        public void Model_Calls_Valid_Exists()
        {
            var model = new CallsModel();
            Assert.IsNotNull(model);
        }

        [TestMethod]
        public void Model_Calls_Valid_ExpectTypeRquiredErrors()
        {
            var model = new CallsModel
            {
                Cost = 0
            };

            var results = TestModelHelper.Validate(model);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("The Called field is required.", results[0].ErrorMessage);
        }

        [TestMethod]
        public void Model_Calls_Valid_ExpectNotPhoneNumberErrors()
        {
            var model = new CallsModel
            {
               Called = "test"
            };

            var results = TestModelHelper.Validate(model);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("Not a valid Phone number", results[0].ErrorMessage);
        }

        [TestMethod]
        public void Model_CallCharge_Valid_Exists()
        {
            var model = new CallChargeModel();
            Assert.IsNotNull(model);
        }

        [TestMethod]
        public void Model_Period_Valid_Exists()
        {
            var model = new PeriodModel();
            Assert.IsNotNull(model);
        }

        [TestMethod]
        public void Model_Statement_Valid_Exists()
        {
            var model = new StatementModel();
            Assert.IsNotNull(model);
        }

        [TestMethod]
        public void Model_Bill_Valid_Exists()
        {
            var model = new BillModel();
            Assert.IsNotNull(model);
        }
    }
}
