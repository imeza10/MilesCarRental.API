using Microsoft.EntityFrameworkCore;
using Service.Common.Response;
using Vehicle.Persistence.Database;
using Vehicle.Service.Queries.BO;
using Vehicle.Service.Queries.DTOs;
using Vehicle.Service.Queries.Repository.Interface.IVehicles;


namespace Vehicle.Service.Queries.ServiceQueries
{
    public class VehicleQueryService : IReadVehicles
    {
        private readonly ApplicationDbContext _context;
        public BOVehicle vehicleObj;

        //Inyección de dependencias
        public VehicleQueryService(ApplicationDbContext context)
        {
            _context = context;
            vehicleObj = new(_context);
        }

        /// <summary>
        /// Consulta todos los vehiculos disponibles. 
        /// </summary>
        /// <returns>Retorna resultado de la Petición.</returns>
        /// <author>Ismael Meza Castillo</author>
        public PetitionResponse GetAllVehicles()
        {
            try
            {
                List<VehicleDTO> vehicles = vehicleObj.GetAllVehicles();

                if (vehicles != null)
                    return new PetitionResponse { Success = true, Message = "Vehiculos encontrados", Module = "Vehicles", Result = vehicles };
                else
                    return new PetitionResponse { Success = false, Message = "No hay vehiculos registrados.", Module = "Vehicles" };
            }
            catch (Exception ex)
            {
                return new PetitionResponse { Success = false, Message = $"Intenta nuevamente, error: { ex}", Module = "Vehicles" };
            }
            
        }

        /// <summary>
        /// Consulta un vehiculo por su ID 
        /// </summary>
        /// <param name="id">ID tipo Guid del vehiculo a buscar.</param>
        /// <returns>Retorna resultado de la Petición.</returns>
        /// <author>Ismael Meza Castillo</author>
        public PetitionResponse GetVehicleById(Guid id)
        {
            try
            {
                VehicleDTO vehicles = vehicleObj.GetVehicleById(id);

                if (vehicles == null)
                    return new PetitionResponse { Success = false, Message = "Vehiculo no encontrado.", Module = "Vehicles" };

                return new PetitionResponse { Success = true, Message = "Vehiculo encontrado", Module = "Vehicle", Result = vehicles };
            }
            catch (Exception ex)
            {
                return new PetitionResponse { Success = false, Message = "El vehiculo no fue encontrado, error: " + ex, Module = "Vehicle" };
            }
        }

        /// <summary>
        /// Consulta una lista de vehiculos disponibles por la localidad de recogida, devolucion o ubicación del cliente.
        /// </summary>
        /// <param name="locationPickUpCode">Codigo de la localidad de Recogida del vehiculo</param>
        /// <param name="locationDeliveryCode">Codigo de la localidad de Devolucion del vehiculo</param>
        /// <param name="locationCustomerCode">Codigo de la localidad del cliente</param>
        /// <returns>Retorna una lista de objetos VehicleDTO, con los datos principales de los vehiculo.</returns>
        /// <author>Ismael Meza Castillo</author>
        public PetitionResponse GetVehicleByLocation(string? locationPickUpCode, string? locationDeliveryCode, string? locationCustomerCode)
        {
            try
            {
                List<VehicleDTO> listVehicles = vehicleObj.GetVehicleByLocation(locationPickUpCode, locationDeliveryCode, locationCustomerCode);

                if (listVehicles == null)
                    return new PetitionResponse { Success = false, Message = "Vehiculos no disponibles.", Module = "Vehicles" };

                //Realizamos la busqueda de vehiculos disponibles según los filtros enviados, ya sea por localidad de recogida, devolución o teniendo en cuenta la ubicación del cliente.                
                return new PetitionResponse { Success = true, Message = "Vehiculos encontrados", Module = "Vehicle", Result = listVehicles };
            }
            catch (Exception ex)
            {
                return new PetitionResponse { Success = false, Message = "Intenta nuevamente, error: " + ex, Module = "Vehicle" };
            }
            
        }
    }
}
