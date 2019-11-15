using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IndividualCapstone.Models
{
    public class ServiceLevel
    {
        [Key]
        public int Id { get; set; }
        public string TypeOfServiceLevel { get; set; }
        public decimal CostOfServiceLevel { get; set; }

    


    }
}