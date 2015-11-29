using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using SkyBillViewer.Models;

namespace SkyBillViewer.Models
{
    public class PackageModel
    {
        [JsonProperty("subscriptions")]
        public IEnumerable<SubscriptionsModel> Subscriptions { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal Total { get; set; }
    }
}