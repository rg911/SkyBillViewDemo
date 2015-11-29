using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Web;
using System.Web.Caching;

namespace SkyBillViewer.Web.Caching
{
    #region Constants
    public class WebConfig
    {
        public const string ServerFarmUrls = "SkyBillViewer.Web.Caching.ServerFarmUrls";
    }

    public class QueryString
    {
        public const string Key = "key";
        public const string Notify = "notify";
    }
    #endregion

    public class CacheHandler : IHttpHandler
    {
        /// <summary>
        /// Use this object to lock the cache whilst checking / adding
        /// </summary>
        public static object LockObject = new object();

        #region IHttpHandler Members
        bool IHttpHandler.IsReusable
        {
            get { return false; }
        }

        void IHttpHandler.ProcessRequest(HttpContext context)
        {
            var keyStartsWith = context.Request.QueryString[QueryString.Key];
            var notify = Convert.ToBoolean(context.Request.QueryString[QueryString.Notify]);
            ClearCache(keyStartsWith, notify);
        }
        #endregion

        //Public Static Methods

        #region Public Static Method - Invalidate
        /// <summary>
        /// Clears the cache for the specified key, or clears the entire cache if key is blank - also notifies all other servers in the farm to do the same
        /// </summary>
        /// <param name="key">The cache key to be invalidated</param>
        public static void Invalidate(string keyStartsWith)
        {
            //remove key from this server's cache
            ClearCache(keyStartsWith, true);            
        }
        #endregion

        #region Public Static Method - NotifyServers
        /// <summary>
        /// Notifies other servers of cache invalidation (comma separated list in the ServerFarmURLs setting in the web.config)
        /// </summary>
        /// <param name="key">The cache key to be cleared</param>
        public static void NotifyServers(string keyStartsWith)
        {
            var otherServers = ConfigurationManager.AppSettings[WebConfig.ServerFarmUrls];
            if (!string.IsNullOrEmpty(otherServers))
            {
                var listURLs = otherServers.Split(',');
                foreach (var url in listURLs)
                {
                    try
                    {
                        var newURL = url;
                        newURL = url + "/cache.clear?notify=false&key=" + HttpContext.Current.Server.UrlEncode(keyStartsWith);

                        var webRequest = (HttpWebRequest)WebRequest.Create(newURL);
                        webRequest.Method = "GET";
                        webRequest.BeginGetResponse(null, null); //backgrounded - don't care about response
                    }
                    catch (Exception)
                    {
                        //TODO: Logging
                        //Elmah.Elmah.LogError(ex, "Error in CacheManagement.NotifyServers");
                    }
                }
            }
        }
        #endregion
        
        #region Public Static Method - Get
        /// <summary>
        /// Retrieves the specified item from the cache or executes a function to generate the data and then adds it to the cache
        /// </summary>
        /// <param name="key">The identifier for the cache item to retrieve</param>
        /// <param name="functionToGetDataToCache">The function which returns the data to be cached</param>
        /// <param name="absoluteExpirationDatetime">The absolute expiry datetime</param>
        /// <param name="slidingExpirationTimespan">The sliding expiry timespan</param>
        /// <returns>The cached item</returns>
        public static object Get(string key, Func<object> functionToGetDataToCache, DateTime absoluteExpirationDatetime = default(DateTime), TimeSpan slidingExpirationTimespan = default(TimeSpan))
        {
            //try to get from cache
            var cachedObject = HttpRuntime.Cache.Get(key);
            if (cachedObject != null) return cachedObject;

            lock (LockObject)
            {
                //not in cache - but may be now - so check again
                cachedObject = HttpRuntime.Cache.Get(key);
                if (cachedObject != null) return cachedObject;

                //still not in cache - run function and cache the results
                cachedObject = functionToGetDataToCache();
                if (cachedObject == null) return null;

                //cache results
                absoluteExpirationDatetime = absoluteExpirationDatetime == default(DateTime) ? Cache.NoAbsoluteExpiration : absoluteExpirationDatetime;
                slidingExpirationTimespan = slidingExpirationTimespan == default(TimeSpan) ? Cache.NoSlidingExpiration : slidingExpirationTimespan;

                HttpRuntime.Cache.Add(key, cachedObject, null, absoluteExpirationDatetime, slidingExpirationTimespan, CacheItemPriority.Normal, null);
            }
            return cachedObject;
        }
        #endregion

        #region Public Static Method - ListForSiteTools
        public static Dictionary<string, string> ListForSiteTools()
        {
            var list = new Dictionary<string,string>();
            var cache = HttpRuntime.Cache.GetEnumerator();

            while (cache.MoveNext())
            {
                if (!cache.Key.ToString().StartsWith("System.Web.Optimization.Bundle")
                    && !cache.Key.ToString().StartsWith("mini-profiler-")
                    && !cache.Key.ToString().StartsWith("__AppStartPage__"))
                {
                    list.Add(cache.Key.ToString(), cache.Value.ToString());
                }
            }

            return list;
        }
        #endregion

        //Private Static Methods

        #region Private Static Method - ClearCache
        /// <summary>
        /// Clears the cache for the specified key, or clears the entire cache if key is blank
        /// </summary>
        /// <param name="key">The cache key to be cleared</param>
        private static void ClearCache(string keyStartsWith, bool notify = true)
        {
            lock (LockObject)
            {
                var cache = HttpRuntime.Cache.GetEnumerator();

                if (string.IsNullOrEmpty(keyStartsWith)) {
                    //empty key - clear everything from the cache
                    while (cache.MoveNext())
                    {
                        HttpRuntime.Cache.Remove(cache.Key.ToString());
                    }
                }
                else
                {
                    //key given - clear an individual item
                    while (cache.MoveNext())
                    {
                        if (cache.Key.ToString().ToLower().StartsWith(keyStartsWith.ToLower()))
                        {
                            HttpRuntime.Cache.Remove(cache.Key.ToString());
                        }
                    }
                }
            }
            if (notify)
            {
                NotifyServers(keyStartsWith);
            }
        }
        #endregion
    }
}
