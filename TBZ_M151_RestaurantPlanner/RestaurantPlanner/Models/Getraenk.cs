using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantPlanner.Models
{
    public class Getraenk
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        public virtual List<Menu> MenuZugehoerigkeit { get; set; }

        public virtual List<Tageskarte> Tageskarten { get; set; }
    }
}