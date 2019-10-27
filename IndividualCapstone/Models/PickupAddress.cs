using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IndividualCapstone.Models
{
    public class PickupAddress
    {
        [Key]
        public int Id { get; set; }
        public string PickupZip { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Pickup Date")]
        public DateTime PickupDate { get; set; }
        public string StreetAddress { get; set; }
        public string PickupTimeWindow { get; set; }
        //address
        [ForeignKey("Shipment")]
        public int? ShipmentId { get; set; }
        public Shipment Shipment { get; set; }
        // ask about this< if public Shipment Ship {get; set;} is needed
        public virtual List<Shipment> Shipments
        {
            get { return _Shipments; }
            set { _Shipments = value; }
        }
        private List<Shipment> _Shipments;

        public string ShipmentSerialized
        {
            get { return JsonConvert.SerializeObject(_Shipments); }
            set { _Shipments = JsonConvert.DeserializeObject<List<Shipment>>(value); }
        }

    }
}