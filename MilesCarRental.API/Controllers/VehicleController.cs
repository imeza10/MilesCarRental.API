using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.Common.Response;
using Vehicle.Service.EventHandlers.Commands;
using Vehicle.Service.Queries.DTOs;
using Vehicle.Service.Queries.Repository.Interface.IVehicles;
using Vehicle.Service.Queries.ServiceQueries;


namespace MilesCarRental.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {

        private readonly ILogger<VehicleController> _logger;
        private readonly IReadVehicles _readVehicleService;
        private readonly IMediator _mediator;


        public VehicleController(ILogger<VehicleController> logger, IReadVehicles readVehicleService, IMediator mediator)
        {
            _logger = logger;
            _readVehicleService = readVehicleService;
            _mediator = mediator;
        }

        /// <summary>
        /// Consulta todos los vehiculos registrados
        /// </summary>
        /// <returns>Retorna un objeto de la clase VehicleDTO, con todos los datos de los vehiculos</returns>
        /// <author>Ismael Meza Castillo</author>
        [HttpGet]
        [Route(nameof(VehicleController.GetAllVehicles))]
        public PetitionResponse GetAllVehicles()
        {
            return _readVehicleService.GetAllVehicles();
        }

        /// <summary>
        /// Consulta un vehiculo por su ID
        /// </summary>
        /// <param name="id">ID de vehiculo a consultar</param>
        /// <returns>Retorna un objeto de la clase VehicleDTO, con todos los datos del vehiculo.</returns>
        /// <author>Ismael Meza Castillo</author>
        [HttpGet]
        [Route(nameof(VehicleController.GetVehicleById))]
        public PetitionResponse GetVehicleById(Guid id)
        {
            return _readVehicleService.GetVehicleById(id);
        }

        /// <summary>
        /// Consulta una lista de vehiculos disponibles por la localidad de recogida, devolucion o ubicación del cliente.
        /// </summary>
        /// <param name="locationPickUpCode">Codigo de la localidad de Recogida del vehiculo</param>
        /// <param name="locationDeliveryCode">Codigo de la localidad de Devolucion del vehiculo</param>
        /// <param name="locationCustomerCode">Codigo de la localidad del cliente</param>
        /// <returns>Retorna una lista de objetos VehicleDTO, con los datos principales de los vehiculo.</returns>
        /// <author>Ismael Meza Castillo</author>
        [HttpGet]
        [Route(nameof(VehicleController.GetVehicleByLocation))]
        public PetitionResponse GetVehicleByLocation(string? locationPickUpCode, string? locationDeliveryCode, string? locationCustomerCode )
        {
            return _readVehicleService.GetVehicleByLocation(locationPickUpCode, locationDeliveryCode, locationCustomerCode);
        }

        /// <summary>
        /// Crea un vehiculo nuevo asignandole un ID nuevo
        /// </summary>
        /// <param name="command">Este objeto contiene la información del vehiculo a crear.</param>
        /// <returns>Resultado de la petición.</returns>
        /// <author>Ismael Meza Castillo</author>
        [HttpPost]
        [Route(nameof(VehicleController.CreateVehicle))]
        public async Task<IActionResult> CreateVehicle(VehicleCreateCommand command)
        {
            await _mediator.Publish(command);
            return Ok();
        }

        /// <summary>
        /// Actualiza un vehiculo según su ID
        /// </summary>
        /// <param name="command">Este objeto contiene la información del vehiculo a actualizar.</param>
        /// <returns>Resultado de la petición.</returns>
        /// <author>Ismael Meza Castillo</author>
        [HttpPut]
        [Route(nameof(VehicleController.UpdateVehicle))]
        public async Task<IActionResult> UpdateVehicle(VehicleUpdateCommand command)
        {
            await _mediator.Publish(command);
            return Ok();
        }
    }
}
