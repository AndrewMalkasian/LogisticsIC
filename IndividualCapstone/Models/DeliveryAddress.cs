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
        public string DeliveryZip { get; set; }
        public string DeliveryTimeWindow { get; set; }
        //FOREIGN KEYS

   // ask about this< if public Shipment Ship {get; set;} is needed
        public virtual List<Shipment> Shipments
        {
            get { return _Shipments; }
            set { _Shipments = value; }
        }
        private List<Shipment> _Shipments;

        public string PackagesSerialized
        {
            get { return JsonConvert.SerializeObject(_Shipments); }
            set { _Shipments = JsonConvert.DeserializeObject<List<Shipment>>(value); }
        }

    }
}