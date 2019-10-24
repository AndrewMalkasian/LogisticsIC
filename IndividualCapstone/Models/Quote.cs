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
        public int PackageCost { get; set; }
        public int PickupCost { get; set; }
        public int DeliveryCost { get; set; }
        public string ServiceLevel { get; set; }
        public int ServiceLevelCost { get; set; }
        public int ShipmentCost { get; set; }
        //method? inside here? 
        //this has a shipmentId
        //total costs
        
        //Quote# = keyId of Shipment# + 10000
    }
}