using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle.Service.Queries.DTOs
{
    public class LocationDTO
    {
        public Guid ID { get; set; }
        public required string Code { get; set; }
        public required string Description { get; set; }
    }
}
