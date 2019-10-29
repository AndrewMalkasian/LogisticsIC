using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IndividualCapstone.Models
{
    public class ServiceArea
    {
        [Key]
        public int Id { get; set; }
        // the A-D and Y are distances based off of the Logistics company's
        //Address. 
        [ForeignKey("ServiceAreaPoint")]
        public int? ServiceAreaPointId { get; set; }
        public Shipment Shipment { get; set; }

        public double Distance { get; set; } 
        public double CostOfServiceAreaPoint { get; set; }
    }
}