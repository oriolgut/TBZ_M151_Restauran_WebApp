using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantPlanner.Models
{
    public class Tageskarte
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TageskarteId { get; set; }

        [Required]
        public DateTime Ablaufdatum { get; set; }

        [Required]
        public virtual Menu MenuZugehoerigkeit { get; set; }

        public virtual List<Gericht> Gerichte { get; set; }

        public virtual List<Getraenk> Getraenke { get; set; }
    }
}