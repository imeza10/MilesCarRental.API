using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicle.Domain;
using Vehicle.Persistence.Database.Configuration;

namespace Vehicle.Persistence.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<Domain.Vehicle> Vehicles { get; set; }
        public DbSet<Location> Locations { get; set; }
        
        protected override void OnModelCreating (ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Domain.Vehicle>()
            .HasOne(v => v.LocationPickUpNavigation)
            .WithMany(l => l.VehiclesPickUpNavigation)
            .HasForeignKey(v => v.LocationPickUp);

            modelBuilder.Entity<Domain.Vehicle>()
                .HasOne(v => v.LocationDeliveryNavigation)
                .WithMany(l => l.VehiclesDeliveryNavigation)
                .HasForeignKey(v => v.LocationDelivery);

            base.OnModelCreating (modelBuilder);
            ModelConfig(modelBuilder);
        }

        private void ModelConfig(ModelBuilder modelBuilder)
        {
            _ = new VehicleConfiguration(modelBuilder.Entity<Domain.Vehicle>());
            _ = new LocationConfiguration(modelBuilder.Entity<Location>());
        }
    }
}
