using Service.Common.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicle.Persistence.Database;
using Vehicle.Service.Queries.DTOs;

namespace Vehicle.Service.Queries.BO
{
    public class BOVehicle
    {
        private readonly ApplicationDbContext _context;

        public BOVehicle(ApplicationDbContext context)
        {
            _context = context;
        }

        #region Queries

        //Función que permite consultar todos los vehiculos 
        public List<VehicleDTO> GetAllVehicles()
        {
            return PostProcessDataVehicleDTO(GetSelectVehicleDTO(_context.Vehicles));
        }

        public VehicleDTO GetVehicleById(Guid id)
        {
            return PostProcessDataVehicleDTO(GetSelectVehicleDTO(_context.Vehicles.Where(x => x.ID == id))).FirstOrDefault() ?? new VehicleDTO();
        }

        public List<VehicleDTO> GetVehicleByLocation(string? locationPickUpCode, string? locationDeliveryCode, string? locationCustomerCode)
        {
            //Usamos Iquerable para preparar la consulta y filtrar los datos lo mas posible para el performance de la consulta.
            IQueryable<Domain.Vehicle> vehicle = _context.Vehicles;

            //Filtramos por localidad de recogida y ubicacion del cliente
            vehicle = GetFilterByLocationPickUpOrCustomer(vehicle, locationPickUpCode, locationCustomerCode);

            //Filtramos por localidad de devolución
            vehicle = GetFilterByLocationDelivery(vehicle, locationDeliveryCode);

            //Hacemos un select de las propiedades que necesitamos y hacemos un PostProcesamiendo de los datos.
            return PostProcessDataVehicleDTO(GetSelectVehicleDTO(vehicle));
        }

        #endregion

        #region Static Methods
        public static IQueryable<Domain.Vehicle> GetFilterByLocationPickUpOrCustomer(IQueryable<Domain.Vehicle> vehicle, string? locationPickUpCode, string? locationCustomerCode)
        {
            //Si el parametro no viene vacio, buscamos por localidad de recogida.
            if (!string.IsNullOrEmpty(locationPickUpCode))
            {
                //Buscamos adicionalmente por localidad de recogida y localidad de recogida según ubicación del cliente
                if (!string.IsNullOrEmpty(locationCustomerCode))
                    vehicle = vehicle.Where(x => x.LocationPickUpNavigation.Code == locationPickUpCode || x.LocationPickUpNavigation.Code == locationCustomerCode);
                else
                    vehicle = vehicle.Where(x => x.LocationPickUpNavigation.Code == locationPickUpCode);
            }

            return vehicle;
        }

        public static IQueryable<Domain.Vehicle> GetFilterByLocationDelivery(IQueryable<Domain.Vehicle> vehicle, string? locationDeliveryCode)
        {
            //Si el parametro no viene vacio, buscamos por localidad de devolucion
            if (!string.IsNullOrEmpty(locationDeliveryCode))
                vehicle = vehicle.Where(x => x.LocationDeliveryNavigation.Code == locationDeliveryCode);

            return vehicle;
        }

        public static List<VehicleDTO> GetSelectVehicleDTO(IQueryable<Domain.Vehicle> vehicle)
        {
            //Hacemos un select de las propiedades que necesitamos, para no traemos toda la entidad, tener la consulta lo mas plana posible y ayudar al performance de la aplicación.
            return vehicle.Select(x => new VehicleDTO
            {
                ID = x.ID,
                Name = x.Name,
                Description = x.Description,
                Plate = x.Plate,
                LocationPickUp = x.LocationPickUp,
                DescriptionLocationPickUp = x.LocationPickUpNavigation.Description,
                State = x.State,
                RegisterAt = x.RegisterAt,
                UserRegister = x.UserRegister,
                UpdateAt = x.UpdateAt,
                UserUpdate = x.UserUpdate,
                LocationDelivery = x.LocationDelivery,
                DescriptionLocationDelivery = x.LocationDeliveryNavigation.Description,
            }).ToList();//Despues del select, nos traemos a memoria con el ToList() los datos para hacer un post procesamiento si es necesario y finalmente retornarlos
        } 
        
        public static List<VehicleDTO> PostProcessDataVehicleDTO(List<VehicleDTO> listVehicle)
        {
            //Cuando tenemos la consulta en memoria, hacemos procesamiento de los datos según la necesidad.
            return listVehicle.Select(x => new VehicleDTO
            {
                ID = x.ID,
                Name = x.Name,
                Description = x.Description?.ToString(),
                Plate = x.Plate?.ToString(),
                LocationPickUp = x.LocationPickUp,
                DescriptionLocationPickUp = x.DescriptionLocationPickUp?.ToString(),
                State = x.State,
                StateDescription = x.State == 1 ? "Disponible" : "No disponible",
                UserRegister = x.UserRegister,
                RegisterAt = x.RegisterAt,
                RegisterAtShort = Convert.ToDateTime(x.RegisterAt).ToShortDateString(),
                UpdateAt = x.UpdateAt,
                UpdateAtShort = Convert.ToDateTime(x.UpdateAt).ToShortDateString(),
                UserUpdate = x.UserUpdate,
                LocationDelivery = x.LocationDelivery,
                DescriptionLocationDelivery = x.DescriptionLocationDelivery?.ToString(),
            }).ToList();
        }

        #endregion


    }
}
