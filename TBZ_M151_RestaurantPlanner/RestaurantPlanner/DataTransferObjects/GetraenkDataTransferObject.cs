using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestaurantPlanner.DataTransferObjects
{
    public class GetraenkDataTransferObject
    {
        public int GetraenkId { get; set; }

        [Required]
        public string GetraenkName { get; set; }

        [Required, Range(minimum: 0.1D, maximum: double.MaxValue)]
        public double GetraenkPreis { get; set; }

        [DefaultValue(false)]
        public bool HeissesGetraenk { get; set; }

        [DefaultValue(false)]
        public bool AlkoholischesGetraenk { get; set; }

        [Required, Range(minimum: 0.04D, maximum: 0.7D)]
        public double GetraenkMenge { get; set; }

        [Required]
        public string MenuZugehoerigkeit { get; set; }

    }
}