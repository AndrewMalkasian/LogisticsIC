using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IndividualCapstone.Models
{
    public class ServiceArea
    {
        [Key]
        public int Id { get; set; }
        // the A-D and Y are distances based off of the Logistics company's
        //Address. 
        public char ServiceAreaPoint{ get; set; }
        public double Distance { get; set; } 
        public double CostOfServiceAreaPoint { get; set; }
    }
}