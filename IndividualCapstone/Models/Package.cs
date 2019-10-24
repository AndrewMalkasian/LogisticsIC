using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IndividualCapstone.Models
{
    public class Package
    {
        [Key]
        public int Id { get; set; }
        public int Weight { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int DimensionalWeight { get; set; }
        public int DimFactor { get; set; }

        [ForeignKey("Shipment")]
        public int? ShipmentId { get; set; }
        public Shipment Shipment { get; set; }

    }
}