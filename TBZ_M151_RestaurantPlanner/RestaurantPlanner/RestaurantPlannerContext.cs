using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using RestaurantPlanner.Models;

namespace RestaurantPlanner
{
    public class RestaurantPlannerContext : DbContext
    {
        public RestaurantPlannerContext() : base("RestaurantPlannerEntities") {}

        public DbSet<Menu> Menus { get; set; }

        public DbSet<Gericht> Gerichte { get; set; }

        public DbSet<Getraenk> Getraenke { get; set; }

        public DbSet<Tageskarte> Tageskarten { get; set; }
    }
}