using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IndividualCapstone.Models
{
    public class ServiceType
    {
        [Key]
        public int Id { get; set; }
        public string NameOfService { get; set; }
        public decimal CostOfService { get; set; }
      
    }
}