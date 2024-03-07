using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle.Persistence.Database.Configuration
{
    public class VehicleConfiguration
    {
        public VehicleConfiguration(EntityTypeBuilder<Domain.Vehicle> entityTypeBuilder)
        {
            entityTypeBuilder.HasIndex(x => x.ID);
            entityTypeBuilder.Property(x => x.Name).IsRequired();
            entityTypeBuilder.Property(x => x.Description).IsRequired();

            var vehicleList = new List<Domain.Vehicle>();
            var idUser = new Guid();            
            var idLocation = new Guid();

            for (int i = 1; i <= 20; i++)
            {
                vehicleList.Add(new Domain.Vehicle
                {
                    ID = new Guid(),
                    Name = $"Vehicle {i}",
                    Description = $"Description  {i}",
                    Plate = $"QHM00{i}",
                    LocationPickUp= idLocation,
                    LocationDelivery= idLocation,
                    State = 1,
                    RegisterAt = new DateTime(),
                    UserRegister = idUser,
                    UpdateAt = new DateTime(),
                    UserUpdate = idUser,

                });
            }
        }
    }
}
