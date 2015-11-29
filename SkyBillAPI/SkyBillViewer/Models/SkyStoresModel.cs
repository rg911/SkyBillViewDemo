using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using SkyBillViewer.Models;

namespace SkyBillViewer.Models
{
    public class SkyStoresModel
    {
        [JsonProperty("rentals")]
        public IEnumerable<StoreRentalsModel> Rentals { get; set; }
        [JsonProperty("buyAndKeep")]
        public IEnumerable<StoreBuyAndKeepModel> BuyAndKeep { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal Total { get; set; }
    }
}