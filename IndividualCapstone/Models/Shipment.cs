using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IndividualCapstone.Models
{
    public class Shipment
    {
        [Key]
        public int Id { get; set; }
        //FREIGHT
        public Package Package { get; set; }

        //DELIVERY ADDRESS
        [ForeignKey("DeliveryAddress")]
        public int? DeliveryAddressId { get; set; }
        public DeliveryAddress DeliveryAddress { get; set; }
        //PICKUP ADDRESS
        [ForeignKey("PickupAddress")]
        public int? PickupAddressId { get; set; }
        public PickupAddress PickupAddress { get; set; }
        //dockToDock, basic, 1man, 2man, Premier...
        [ForeignKey("ServiceType")]
        public int? ServiceTypeId { get; set; }
        public ServiceType ServiceType { get; set; }
        // SameDay, 1-3day, Economy
        [ForeignKey("ServiceLevel")]
        public int? ServiceLevelId { get; set; }
        public ServiceLevel ServiceLevel { get; set; }
        // A-D + Area Y from Logistics Hub
        public string ServiceAreaPoint { get; set; }
        
        //my costs public method inside?
        [ForeignKey("Quote")]
        public int? QuoteId { get; set; }
        public Quote Quote { get; set; }

        // [NotMapped]
        public IEnumerable<ServiceLevel> LevelOfServiceList { get; set; }
        public IEnumerable<ServiceType> ServiceTypeList { get; set; }

    }
}