using BarberLegacy.Api.DTOs.Barber;
using BarberLegacy.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberLegacy.Api.Controllers
{
    [ApiController]
    [Route("api/barbers")]
    public class BarbersController : ControllerBase
    {
        private readonly IBarberService _barberService;

        public BarbersController(IBarberService barberService)
        {
            _barberService = barberService;
        }

        [HttpGet]
        [EndpointSummary("Obtiene todos los barberos")]
        [ProducesResponseType(typeof(IEnumerable<BarberResponseDto>), StatusCodes.Status200OK)]
        public async Task<IEnumerable<BarberResponseDto>> GetAll()
        {
            var barbers = await _barberService.GetAllAsync();
            return barbers;
        }

        [HttpGet("{id:int}")]
        [EndpointSummary("Obtiene un barbero específico por ID")]
        [ProducesResponseType(typeof(BarberResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BarberResponseDto>> GetById(int id)
        {
            var barber = await _barberService.GetByIdAsync(id);

            if (barber is null)
            {
                return NotFound(new { message = $"El barbero con ID {id} no fue encontrado." });
            }

            return Ok(barber);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Barber")]
        [EndpointSummary("Crea un nuevo barbero")]
        [EndpointDescription("Solo administradores o barberos pueden crear nuevos perfiles.")]
        [ProducesResponseType(typeof(BarberResponseDto), StatusCodes.Status201Created)]
        public async Task<ActionResult<BarberResponseDto>> Create([FromBody] BarberCreateDto barber)
        {
            var createdBarber = await _barberService.CreateAsync(barber);

            return CreatedAtAction(nameof(GetById), new { id = createdBarber.Id }, createdBarber);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin, Barber")]
        [EndpointSummary("Actualiza un barbero existente")]
        [ProducesResponseType(typeof(BarberResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BarberResponseDto>> Update(int id, [FromBody] BarberUpdateDto barber)
        {
            var updatedBarber = await _barberService.UpdateAsync(id, barber);

            if (updatedBarber is null)
            {
                return NotFound(new { message = $"No se pudo actualizar. El barbero con ID {id} no existe." });
            }

            return Ok(updatedBarber);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin, Barber")]
        [EndpointSummary("Elimina un barbero existente")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var delete = await _barberService.DeleteAsync(id);

            if (!delete)
            {
                return NotFound(new { message = $"No se pudo borrar. El barbero con ID {id} no existe." });
            }

            return NoContent();
        }
    }
}