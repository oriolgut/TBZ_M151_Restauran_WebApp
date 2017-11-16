using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestaurantPlanner.DataTransferObjects
{
    public class GerichtDataTransferObject
    {
        public int GerichtId { get; set; }

        [Required]
        public string GerichtName { get; set; }

        [Required, Range(minimum: 0.1D, maximum: double.MaxValue)]
        public double GerichtPreis { get; set; }

        [Required]
        public string MenuZugehoerigkeit { get; set; }

    }
}