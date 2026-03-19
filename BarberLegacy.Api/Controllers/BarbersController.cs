using BarberLegacy.Api.DTOs.Barber;
using BarberLegacy.Api.Services.Interfaces;
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

        public async Task<IEnumerable<BarberResponseDto>> GetAll()
        {
            var barbers = await _barberService.GetAllAsync();
            return barbers;
        }

        [HttpGet("{id:int}")]
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
        public async Task<ActionResult<BarberResponseDto>> Create([FromBody] BarberCreateDto barber)
        {
            var createdBarber = await _barberService.CreateAsync(barber);

            return CreatedAtAction(nameof(GetById), new { id = createdBarber.Id }, createdBarber);
        }


        [HttpPut("{id:int}")]
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
