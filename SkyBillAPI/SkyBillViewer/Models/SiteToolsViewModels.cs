using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyBillViewer.Models
{
    public class CacheViewModel
    {
        public string Message { get; set; }
        public Dictionary<string, string> CacheItems { get; set; }
        public string CacheMemoryFree { get; set; }
    }
}