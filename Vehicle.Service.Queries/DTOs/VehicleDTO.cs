using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle.Service.Queries.DTOs
{
    public class VehicleDTO
    {
        public Guid ID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Plate { get; set; }
        public Guid? LocationPickUp { get; set; }
        public Guid? LocationDelivery { get; set; }
        public int? State { get; set; }
        public DateTime RegisterAt { get; set; }
        public string? RegisterAtShort { get; set; }
        public Guid UserRegister { get; set; }
        public DateTime UpdateAt { get; set; }
        public string? UpdateAtShort { get; set; }
        public Guid UserUpdate { get; set; }
        public string? DescriptionLocationPickUp { get; set; }
        public string? DescriptionLocationDelivery { get; set; }
        public string? StateDescription { get; set; }
    }
}
