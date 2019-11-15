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
        [Display(Name = "Pickup Zip")]
        public string PickupZip { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Pickup Date")]
        public DateTime PickupDate { get; set; }
        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }
        public string PickupTimeWindow { get; set; }
        public string AddressLatLong { get; set; }
        [Display(Name = "Add To Route?")]
        public bool AddToRoute { get; set; }

    }
}