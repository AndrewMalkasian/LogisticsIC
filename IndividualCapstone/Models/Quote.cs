using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IndividualCapstone.Models
{
    public class Quote
    {
        [Key]
        public int Id { get; set; }
        public int QuoteNumber { get; set; }
        public decimal PackageCost { get; set; } // done
        public decimal PickupCost { get; set; }  // done
        public decimal DeliveryCost { get; set; } // done
        public decimal ServiceLevelCost { get; set; } 
        public decimal ServiceTypeCost { get; set; }
        public decimal? BetweenCitiesCost { get; set; }
        public decimal ServiceAreaCost { get; set; } //pickupcost + deliveryCost
        public decimal FuelSurcharge { get; set; } //not needed here
        public decimal? ShipmentCost { get; set; } // total of everything
        public string LogisticsHubZipCode { get; set; }
        [ForeignKey("Customer")]
        public int? CustomerId {get; set;}
        public Customer Customer { get; set; }

        //method? inside here? 
        //this has a shipmentId
        //total costs
        
        //Quote# = keyId of Shipment# + 10000
    }
}