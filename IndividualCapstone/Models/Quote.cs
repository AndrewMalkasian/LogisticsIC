using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IndividualCapstone.Models
{
    public class Quote
    {
        [Key]
        public int Id { get; set; }
        public double PackageCost { get; set; }
        public double PickupCost { get; set; }
        public double DeliveryCost { get; set; }
        public double ServiceLevelCost { get; set; }
        public double ServiceTypeCost { get; set; }
        public double ServiceAreaCost { get; set; }
        public double FuelSurcharge { get; set; }
        public double ShipmentCost { get; set; }

        //method? inside here? 
        //this has a shipmentId
        //total costs
        
        //Quote# = keyId of Shipment# + 10000
    }
}