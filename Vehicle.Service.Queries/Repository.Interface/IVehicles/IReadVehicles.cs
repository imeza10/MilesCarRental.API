using Service.Common.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle.Service.Queries.Repository.Interface.IVehicles
{
    public interface IReadVehicles
    {
        public PetitionResponse GetAllVehicles();
        public PetitionResponse GetVehicleById(Guid id);
        public PetitionResponse GetVehicleByLocation(string? locationPickUpCode, string? locationDeliveryCode, string? locationCustomerCode);
    }
}
