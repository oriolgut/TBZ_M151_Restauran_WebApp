using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using RestaurantPlanner.Models;

namespace RestaurantPlanner.DAL
{
    public class RestaurantPlannerContext : DbContext
    {
        public RestaurantPlannerContext() : base("RestaurantPlannerEntities")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<RestaurantPlannerContext>());
        }

        public DbSet<Menu> Menus { get; set; }

        public DbSet<Gericht> Gerichte { get; set; }

        public DbSet<Getraenk> Getraenke { get; set; }

        public DbSet<Tageskarte> Tageskarten { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}