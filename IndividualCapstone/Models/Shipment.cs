using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IndividualCapstone.Models
{
   public class Shipment
   {
            [Key]
            public int Id { get; set; }
            public int Pieces { get; set; }
            public string AddressInput { get; set; }
            public Package Package { get; set; }
                                        
        //fk1 _ 2
        [ForeignKey("DeliveryAddress")]
            public int? DeliveryAddressId { get; set; }
            public DeliveryAddress DeliveryAddress { get; set; }
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
            [ForeignKey("ServiceArea")]
            public int? ServiceAreaId { get; set; }
            public ServiceArea ServiceArea { get; set; }

            //my costs public method inside?
            [ForeignKey("Quote")]
            public int? QuoteId { get; set; }
            public Quote Quote { get; set; }

   }
}




//public virtual List<Package> Packages
//{
//    get { return _Packages; }
//    set { _Packages = value; }
//}
//private List<Package> _Packages;

//public string PackagesSerialized
//{
//    get { return JsonConvert.SerializeObject(_Packages); }
//    set { _Packages = JsonConvert.DeserializeObject<List<Package>>(value); }
//}
