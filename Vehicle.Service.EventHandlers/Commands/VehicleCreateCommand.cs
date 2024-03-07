using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle.Service.EventHandlers.Commands
{
    public class VehicleCreateCommand : INotification
    {
        public Guid ID { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required string Plate { get; set; }
        public Guid? LocationPickUp { get; set; }
        public Guid? LocationDelivery { get; set; }
        public int? State { get; set; }
        public DateTime RegisterAt { get; set; }
        public Guid UserRegister { get; set; }
        public DateTime UpdateAt { get; set; }
        public Guid UserUpdate { get; set; }
    }
}
