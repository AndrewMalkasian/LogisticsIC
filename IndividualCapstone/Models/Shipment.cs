using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using static IndividualCapstone.Models.Quote;

namespace IndividualCapstone.Models
{
    public class Shipment
    {
        [Key]
        public int Id { get; set; }
        public int Pieces { get; set; }
        [Display(Name = "Freight")]

        public virtual List<Shipment> Packages
        {   get   { return _Packages  ;}
            set   {  _Packages = value ;}
        }
        private List<Shipment> _Packages;

        public string PackagesSerialized
        {
            get { return JsonConvert.SerializeObject(_Packages); }
            set { _Packages = JsonConvert.DeserializeObject<List<Shipment>>(value); }
        }
        
        //fk1 _ 2
        [ForeignKey("DeliveryAddress")]
        public int? DeliveryAddressId { get; set; }
        public DeliveryAddress DeliveryAddress { get; set; }
        [ForeignKey("PickupAddress")]
        public int? PickupAddressId { get; set; }
        public PickupAddress PickupAddress { get; set; }

        //my costs public method inside?
        [ForeignKey("Quote")]
        public int? QuoteId { get; set; }
        public Quote Quote { get; set; }
    }
}