using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IndividualCapstone.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set;}
        public string LastName { get; set; }
        public string BusinessName { get; set; }
        public string StreetAddress { get; set; }
        public string StreetAddress2 { get; set; }
        public string ZipCode { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }

        //customer has a quote




    }
}