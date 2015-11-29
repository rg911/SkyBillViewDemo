using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SkyBillViewer.Models
{
    public class SubscriptionsModel
    {
        [Required]
        public string Type { get; set; }
        [Required]
        public string Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal Cost { get; set; }
    }
}