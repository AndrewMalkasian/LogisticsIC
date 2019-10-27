using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IndividualCapstone.Models
{
    public class ShipmentPackageAddressDelAndPickupViewModel
    {
        public Shipment Shipment { get; set; }
        public Package Package { get; set; }
        public PickupAddress PickupAddress { get; set; }
        public DeliveryAddress DeliveryAddress { get; set; }
      

    }
}