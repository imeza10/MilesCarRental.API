using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle.Domain
{
    public class Location
    {
        public Guid ID { get; set; }
        public required string Code { get; set; }
        public required string Description { get; set; }

        // Propiedades de navegación inversa
        public ICollection<Vehicle> VehiclesPickUpNavigation { get; set; } = new List<Vehicle>();
        public ICollection<Vehicle> VehiclesDeliveryNavigation { get; set; } = new List<Vehicle>();
    }
}
