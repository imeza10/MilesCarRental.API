using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicle.Domain;

namespace Vehicle.Persistence.Database.Configuration
{
    public class LocationConfiguration
    {
        public LocationConfiguration(EntityTypeBuilder<Location> entityTypeBuilder)
        {
            entityTypeBuilder.HasIndex(x => x.ID);
            var listLocation = new List<Location>();

            for (int i = 1; i <= 5; i++)
            {
                listLocation.Add(new Location
                {
                    ID = new Guid(),
                    Code = $"Code {i}",
                    Description = $"Description {i}",

                });
            }
        }
    }
}
