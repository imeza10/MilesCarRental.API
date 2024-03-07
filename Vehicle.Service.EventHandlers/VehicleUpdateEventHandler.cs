using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Vehicle.Persistence.Database;
using Vehicle.Service.EventHandlers.Commands;
using Vehicle.Domain;
using MediatR;

namespace Vehicle.Service.EventHandlers
{
    public class VehicleUpdateEventHandler : INotificationHandler<VehicleUpdateCommand>
    {
        private readonly ApplicationDbContext _context;
        public VehicleUpdateEventHandler(ApplicationDbContext contex)
        {
            _context = contex;
        }

        public async Task Handle(VehicleUpdateCommand command, CancellationToken cancellationToken)
        {            
            var vehicle = _context.Vehicles.FirstOrDefault(x => x.ID == command.ID) ?? null;

            if(vehicle != null) { 
                vehicle.Name = command.Name;
                vehicle.Description = command.Description;
                vehicle.Plate = command.Plate;
                vehicle.LocationPickUp = command.LocationPickUp;
                vehicle.LocationDelivery = command.LocationDelivery;
                vehicle.State = command.State;
                vehicle.RegisterAt = command.RegisterAt;
                vehicle.UpdateAt = command.UpdateAt;
                vehicle.UserRegister = command.UserRegister;
                vehicle.UserUpdate = command.UserUpdate;
                
                await _context.SaveChangesAsync();
            }

        }

    }
}
