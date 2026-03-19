using BarberLegacy.Api.DTOs.Services;
using BarberLegacy.Api.Services.Interfaces;
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
        public async Task<IEnumerable<ServiceResponseDto>> GetAll()
        {
            var services = await _serviceService.GetAllAsync();
            return services;
        }

        [HttpGet("{id}")]
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
        public async Task<ActionResult<ServiceResponseDto>> Create([FromBody] ServiceCreateDto dto)
        {
            var createdService = await _serviceService.CreateAsync(dto);

            // Devuelve 201 created y la ruta para consultar el nuevo recurso
            return CreatedAtAction(nameof(GetById), new { id = createdService.Id }, createdService);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ServiceResponseDto>> Update(int id,  [FromBody] ServiceUpdateDto dto)
        {
            var updatedService = await _serviceService.UpdateAsync(id, dto);

            if (updatedService == null)
            {
                return NotFound(new { message = $"No se pudo actualizar. El servicio con ID {id} no existe." });
            }

            return Ok(updatedService);

        }

        [HttpDelete("{id:int}")]
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
