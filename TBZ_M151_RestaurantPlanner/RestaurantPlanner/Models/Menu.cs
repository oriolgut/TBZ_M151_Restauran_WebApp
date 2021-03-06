﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantPlanner.Models
{
    public class Menu
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MenuId { get; set; }

        [Required]
        public string MenuName { get; set; }

        public virtual List<Gericht> Gerichte { get; set; }

        public virtual List<Getraenk> Getraenke { get; set; }
    }
}