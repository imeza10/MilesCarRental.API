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
using Microsoft.Extensions.Logging;

namespace Vehicle.Service.EventHandlers
{
    public class VehicleCreateEventHandler : INotificationHandler<VehicleCreateCommand>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<VehicleCreateEventHandler> _logger;
        public VehicleCreateEventHandler(ApplicationDbContext contex, ILogger<VehicleCreateEventHandler> logger)
        {
            _context = contex;
            _logger = logger;
        }

        public async Task Handle(VehicleCreateCommand command, CancellationToken cancellationToken)
        {
            //_logger.LogInformation("--- VehicleCreateCommand started");

            await _context.AddAsync(new Domain.Vehicle
            {
                ID = new Guid(),
                Name = command.Name,
                Description = command.Description,
                Plate = command.Plate,
                LocationPickUp = command.LocationPickUp,
                LocationDelivery = command.LocationDelivery,
                State = command.State,
                RegisterAt = command.RegisterAt,
                UpdateAt = command.UpdateAt,
                UserRegister = command.UserRegister,
                UserUpdate = command.UserUpdate
            });
            //_logger.LogInformation("--- Se agregó el vehiculo. ");
            await _context.SaveChangesAsync();

            //_logger.LogInformation("--- VehicleCreateCommand ended");
        }

    }
}
