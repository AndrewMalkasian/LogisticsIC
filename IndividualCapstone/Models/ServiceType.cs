using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IndividualCapstone.Models
{
    public class ServiceType
    {
        [Key]
        public int Id { get; set; }
        public string NameOfServiceType { get; set; }
        public double CostOfServiceType { get; set; }
    }
}