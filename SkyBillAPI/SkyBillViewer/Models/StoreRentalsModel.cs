using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SkyBillViewer.Models
{
    public class StoreRentalsModel
    {
        [Required]
        public string Title { get; set; }
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal Cost { get; set; }
    }
}