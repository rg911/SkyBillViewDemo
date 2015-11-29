using System;
using System.Net;
using System.Web.Mvc;
using Newtonsoft.Json;
using SkyBillViewer.Models;
using Web.Extensions;
using SkyBillViewer.Web.Caching;
using System.Web;

namespace SkyBillViewer.Controllers
{
    public class HomeController : Controller
    {
        internal const string  homeMVCCacheKey = "BillData-MVC";

        private readonly string url = "http://safe-plains-5453.herokuapp.com/bill.json"; //For demo only
        /// <summary>
        /// MVC index controller. Cache json reaults and log actions in Glimpse
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            try
            {
                using (var client = new WebClient())
                {
                    string json = client.DownloadString(url);
                    object model;
                    using (GlimpseTimeline.Capture("====> Get bills from Json URL MVC"))
                    {
                        model = CacheHandler.Get(homeMVCCacheKey, () =>
                        {
                            return JsonConvert.DeserializeObject<BillModel>(json);
                        });
                    }
                    
                    return View("Index", model);
                }
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception(ex.Message));
            }

            return View();
        }

        /// <summary>
        /// AngularJS Idex action. No model from mvc control here but will be created in API Controller
        /// </summary>
        /// <returns></returns>
        public ActionResult Index_AngularJS()
        {
            return View("Index_AngularJS");
        }
    }
}
