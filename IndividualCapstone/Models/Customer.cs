using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IndividualCapstone.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string FirstName { get; set;}
        public string LastName { get; set; }
        public string BusinessName { get; set; }
        public string StreetAddress { get; set; }
        public string StreetAddress2 { get; set; }
        public string ZipCode { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }

        [ForeignKey("Shipment")]
        public int? ShipmentId { get; set; }
        public Shipment Shipment { get; set; }
   

        //customer has a quote




    }
}