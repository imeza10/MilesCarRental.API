using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicle.Service.EventHandlers.Commands;

namespace Vehicle.Service.Queries.Repository.Interface.IVehicles
{
    public interface IWriteVehicles
    {
        public Task<IActionResult> CreateVehicle(VehicleCreateCommand command);

        public Task<IActionResult> UpdateVehicle(VehicleUpdateCommand command);
    }
}
