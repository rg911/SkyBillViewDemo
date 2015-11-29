using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace SkyBillViewer.Models
{
    public class BillModel
    {
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal Total { get; set; }

        public PackageModel Package { get; set; }
        [JsonProperty("callCharges")]
        public CallChargeModel CallCharge { get; set; }
        [JsonProperty("skyStore")]
        public SkyStoresModel SkyStore { get; set; }
        [JsonProperty("statement")]
        public StatementModel Statement { get; set; }
    }
}