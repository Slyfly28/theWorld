using System;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;

namespace theWorld.Models
{
    public class WorldContext : IdentityDbContext<WorldUser>
    {
        public WorldContext()
        {
            Database.EnsureCreated();
        }

        public DbSet<Trip> Trips { get; set; } 
        public DbSet<Stop> Stops { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connString = Startup.Configuration["Data:WorldContextConnection"]; // instantiate connection strings

            optionsBuilder.UseSqlServer(connString);

            base.OnConfiguring(optionsBuilder);
        }
    }
}
