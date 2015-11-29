using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using System.Web.Mvc;
using SkyBillViewer.Models;

namespace SkyBillViewer.Tests.Controllers
{
    public class APITestHelper
    {
        public static T GetValueFromJsonResult<T>(object jsonResult, string propertyName)
        {
            var property =
                jsonResult.GetType().GetProperties()
                .Where(p => string.Compare(p.Name, propertyName) == 0)
                .FirstOrDefault();

            if (null == property)
                throw new ArgumentException("propertyName not found", "propertyName");
            return (T)property.GetValue(jsonResult, null);
        }

        public static IEnumerable<SubscriptionsModel> GetSubscriptions()
        {
            var testsub = new List<SubscriptionsModel>();
            testsub.Add(new SubscriptionsModel { Name = "A show", Type = "tv", Cost = 10 });
            testsub.Add(new SubscriptionsModel { Name = "A talk", Type = "talk", Cost = 10 });
            testsub.Add(new SubscriptionsModel { Name = "A broadband", Type = "broadband", Cost = 10 });
            return testsub;
        }

        public static PackageModel GetPackage()
        {
            var testPack = new PackageModel();
            testPack.Subscriptions = GetSubscriptions();
            testPack.Total = 100;
            return testPack;
        }

        public static PeriodModel GetPeriod()
        {
            var testPeriod = new PeriodModel();
            testPeriod.PeriodFrom = DateTime.MinValue;
            testPeriod.PeriodTo = DateTime.MaxValue;
            return testPeriod;
        }

        public static StatementModel GetStatement()
        {
            var testStat = new StatementModel();
            testStat.Period = GetPeriod();
            testStat.Due = DateTime.MinValue;
            testStat.Generated = DateTime.MinValue;
            return testStat;
        }

        public static IEnumerable<StoreRentalsModel> GetStoreRentals()
        {
            var testsub = new List<StoreRentalsModel>();
            testsub.Add(new StoreRentalsModel { Title = "A rent", Cost = 10 });
            testsub.Add(new StoreRentalsModel { Title = "A rent2", Cost = 10 });
            testsub.Add(new StoreRentalsModel { Title = "A rent3", Cost = 10 });
            return testsub;
        }

        public static IEnumerable<StoreBuyAndKeepModel> GetStoreBuyAndKeep()
        {
            var testsub = new List<StoreBuyAndKeepModel>();
            testsub.Add(new StoreBuyAndKeepModel { Title = "A buy", Cost = 10 });
            testsub.Add(new StoreBuyAndKeepModel { Title = "A buy2", Cost = 10 });
            testsub.Add(new StoreBuyAndKeepModel { Title = "A buy3", Cost = 10 });
            return testsub;
        }

        public static SkyStoresModel GetSkyStore()
        {
            var testStore = new SkyStoresModel();
            testStore.Rentals = GetStoreRentals();
            testStore.BuyAndKeep = GetStoreBuyAndKeep();
            testStore.Total = 100;
            return testStore;
        }

        public static IEnumerable<CallsModel> GetCalls()
        {
            var testsub = new List<CallsModel>();
            testsub.Add(new CallsModel { Called = "07777777777", Duration = TimeSpan.MinValue, Cost = 10 });
            testsub.Add(new CallsModel { Called = "07766666666", Duration = TimeSpan.MinValue, Cost = 10 });
            testsub.Add(new CallsModel { Called = "07755555555", Duration = TimeSpan.MinValue, Cost = 10 });
            return testsub;
        }

        public static CallChargeModel GetCallCharge()
        {
            var testCall = new CallChargeModel();
            testCall.Total = 90;
            testCall.Calls = GetCalls();
            return testCall;
        }

        public static BillModel GetBill()
        {
            var testBill = new BillModel();
            testBill.Total = 999;
            testBill.Statement = GetStatement();
            testBill.SkyStore = GetSkyStore();
            testBill.Package = GetPackage();
            testBill.CallCharge = GetCallCharge();

            return testBill;
        }
    }
}
