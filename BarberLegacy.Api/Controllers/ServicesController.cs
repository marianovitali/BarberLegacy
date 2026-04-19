using BarberLegacy.Api.DTOs.Services;
using BarberLegacy.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberLegacy.Api.Controllers
{
    [ApiController]
    [Route("api/services")]
    public class ServicesController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        public ServicesController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpGet]
        [EndpointSummary("Obtiene todos los servicios disponibles")]
        [ProducesResponseType(typeof(IEnumerable<ServiceResponseDto>), StatusCodes.Status200OK)]
        public async Task<IEnumerable<ServiceResponseDto>> GetAll()
        {
            var services = await _serviceService.GetAllAsync();
            return services;
        }

        [HttpGet("{id}")]
        [EndpointSummary("Obtiene un servicio específico por ID")]
        [ProducesResponseType(typeof(ServiceResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ServiceResponseDto>> GetById(int id)
        {
            var service = await _serviceService.GetByIdAsync(id);
            if (service is null)
            {
                return NotFound(new { message = $"El servicio con ID {id} no fue encontrado." });
            }

            return Ok(service);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Barber")]
        [EndpointSummary("Crea un nuevo servicio")]
        [EndpointDescription("Solo administradores o barberos pueden crear nuevos servicios.")]
        [ProducesResponseType(typeof(ServiceResponseDto), StatusCodes.Status201Created)]
        public async Task<ActionResult<ServiceResponseDto>> Create([FromBody] ServiceCreateDto dto)
        {
            var createdService = await _serviceService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = createdService.Id }, createdService);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin, Barber")]
        [EndpointSummary("Actualiza un servicio existente")]
        [ProducesResponseType(typeof(ServiceResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ServiceResponseDto>> Update(int id, [FromBody] ServiceUpdateDto dto)
        {
            var updatedService = await _serviceService.UpdateAsync(id, dto);

            if (updatedService == null)
            {
                return NotFound(new { message = $"No se pudo actualizar. El servicio con ID {id} no existe." });
            }

            return Ok(updatedService);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin, Barber")]
        [EndpointSummary("Elimina un servicio existente")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var delete = await _serviceService.DeleteAsync(id);

            if (!delete)
            {
                return NotFound(new { message = $"No se pudo borrar. El servicio con ID {id} no existe." });
            }

            return NoContent();
        }
    }
}