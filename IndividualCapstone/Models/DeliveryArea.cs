using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IndividualCapstone.Models
{
    public class DeliveryArea
    {
        [Key]
        public int Id { get; set; }
        // the A-D and Y are distances based off of the Logistics company's
        //Address. 
        public string NameOfServiceArea { get; set; }
        public decimal Distance { get; set; }
        public decimal CostOfServiceAreaPoint { get; set; }
    }
}