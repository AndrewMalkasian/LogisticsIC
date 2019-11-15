using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IndividualCapstone.Models
{
    public class DeliveryAddress
    {
        [Key]
        public int Id { get; set; }
        [DataType(DataType.DateTime)]
        [Display(Name = "Delivery Date")]
        public DateTime DeliveryDate { get; set; }
        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }
        [Display(Name = "Delivery Zip")]
        public string DeliveryZip { get; set; }
        public string DeliveryTimeWindow { get; set; }
        public string AddressLatLong { get; set; }
        [Display(Name ="Add To Route?")]
        public bool AddToRoute { get; set; }
        //FOREIGN KEYS
        //
        public virtual List<Shipment> Shipments { get; set; }
    }
}