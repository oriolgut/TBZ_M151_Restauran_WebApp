using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using RestaurantPlanner.Models;

namespace RestaurantPlanner.DataTransferObjects
{
    public class TageskarteDataTransferObject
    {
        public int TageskarteId { get; set; }

        public DateTime Ablaufdatum { get; set; }

        [Required]
        public string MenuZugehoerigkeit { get; set; }

        public List<Gericht> Gerichte { get; set; }

        public List<Getraenk> Getraenke { get; set; }
    }
}