To use this Caching project in your own MVC solution:

Add the following to the top of AppStart/RouteConfig.cs

     //ignore the Web.Caching handler
     routes.IgnoreRoute("{*cache}", new { cache=@"cache.clear" });

If using pipeline mode: Add the following to the <system.webserver><handlers> section of web.config

     <add name="Web.Caching" path="cache.clear" verb="*" type="Web.Caching.CacheHandler, Web.Caching" />

If NOT using pipeline mode: Add the following to the <systemweb><httpHandlers> section of web.config

     <add verb="*" path="cache.clear" type="CacheManagement.CacheHandler, CacheManagement" />

To invalidate a cached item from code, this will notify all other servers to remove this item from the cache:

     CacheManagement.CacheHandler.Invalidate("key--leave-blank-to-clear-entire-cache");
     
Calling cache.clear url will not notify other servers by default - but this can be overridden by adding the notify=true parameter