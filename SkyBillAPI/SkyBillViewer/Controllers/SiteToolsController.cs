using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SkyBillViewer.Models;
using SkyBillViewer.Web.Caching;

namespace SkyBillViewer.Controllers
{
    public class SiteToolsController : Controller
    {
        // GET: SiteTools
        public ActionResult Cache()
        {
            var model = new CacheViewModel();

            if (Request.HttpMethod == "POST")
            {
                if (!string.IsNullOrEmpty(Request.Form["btnFlushCache"]))
                {
                    //flush the entire cache
                    CacheHandler.Invalidate(null);
                    model.Message = "Cache flushed.";
                }
            }

            model.CacheItems = CacheHandler.ListForSiteTools();
            model.CacheMemoryFree = string.Format("Physical memory available for caching: {0}% ({1} MB)", HttpRuntime.Cache.EffectivePercentagePhysicalMemoryLimit.ToString(), (HttpRuntime.Cache.EffectivePrivateBytesLimit / 1024) / 1024);

            return View(model);
        }
    }
}