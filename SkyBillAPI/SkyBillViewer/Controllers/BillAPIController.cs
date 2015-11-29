using System.Web.Mvc;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SkyBillViewer.Models;
using System.Web.Http;
using System.Net.Http;
using System.Net.Http.Formatting;
using Newtonsoft.Json.Serialization;
using System;
using Web.Extensions;
using SkyBillViewer.Web.Caching;

namespace SkyBillViewer.Controllers
{
    /// <summary>
    /// MVC controller for GetBillData. 
    /// !!!!! DEBUG ONLY, NOT USED!!!!!!
    /// </summary>
    public class BillController : Controller
    {
        // GET api/product/5
        public ActionResult GetBillData(string url)
        {
            BillModel model = null;
            using (var client = new WebClient())
            {
                var jsonData = client.DownloadString(url); //"http://safe-plains-5453.herokuapp.com/bill.json"
                model = JsonConvert.DeserializeObject<BillModel>(jsonData, new IsoDateTimeConverter());
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }

    /// <summary>
    /// API controller getting json data from provided URL
    /// </summary>
    public class BillAPIController : ApiController
    {
        internal const string homeAngularJSCacheKey = "BillData-AngujarJS";

        // GET api/Bill
        public object GetBillData(string url)
        {
            JsonResult jsonResult = new JsonResult();
            try
            {
                using (var client = new WebClient())
                {
                    var jsonData = client.DownloadString(url);
                    var jsonSerializerSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
                    object model;
                    using (GlimpseTimeline.Capture("====> Get bills from Json URL AngularJS"))
                    {
                        model = CacheHandler.Get(homeAngularJSCacheKey, () =>
                        {
                            return JsonConvert.DeserializeObject<BillModel>(jsonData, jsonSerializerSettings);
                        });
                    }

                    jsonResult = new JsonResult
                    {
                        Data = model,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet

                    };
                }
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception(ex.Message));
            }

            return jsonResult.Data;
        }
    }
}
