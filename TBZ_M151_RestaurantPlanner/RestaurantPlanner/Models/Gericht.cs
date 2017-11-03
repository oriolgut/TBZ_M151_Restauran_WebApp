using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantPlanner.Models
{
    public class Gericht
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GerichtId { get; set; }

        [Required]
        public string GerichtName { get; set; }

        [Required, Range(minimum: 0.1D, maximum: double.MaxValue)]
        public double GerichtPreis { get; set; }

        public int MenuId { get; set; }

        [Required, ForeignKey(nameof(MenuId))]
        public virtual Menu MenuZugehoerigkeit { get; set; }

        public virtual List<Tageskarte> Tageskarten { get; set; }
    }
}