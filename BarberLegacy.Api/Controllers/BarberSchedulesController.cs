using BarberLegacy.Api.DTOs.Barber;
using BarberLegacy.Api.DTOs.BarberSchedule;
using BarberLegacy.Api.Entities;
using BarberLegacy.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace BarberLegacy.Api.Controllers
{
    [ApiController]
    [Route("api/barberschedules")]
    public class BarberSchedulesController : ControllerBase
    {
        private readonly IBarberScheduleService _barberScheduleService;

        public BarberSchedulesController(IBarberScheduleService barberScheduleService)
        {
            _barberScheduleService = barberScheduleService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BarberScheduleResponseDto>>> GetAll()
        {
            var schedules = await _barberScheduleService.GetAllAsync();
            return Ok(schedules);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<BarberScheduleResponseDto>> GetById(int id)
        {
            var schedule = await _barberScheduleService.GetByIdAsync(id);
            if (schedule is null)
            {
                return NotFound(new { message = $"El horario con ID {id} no fue encontrado." });
            }
            return Ok(schedule);
        }
        [HttpGet("barber/{barberId:int}")]
        public async Task<ActionResult<IEnumerable<BarberScheduleResponseDto>>> GetByBarberId(int barberId)
        {
            var schedules = await _barberScheduleService.GetByBarberIdAsync(barberId);
            return Ok(schedules);
        }

        [HttpPost]
        public async Task<ActionResult<BarberScheduleResponseDto>> Create([FromBody] BarberScheduleCreateDto schedule)
        {
            var createdSchedule = await _barberScheduleService.CreateAsync(schedule);

            return CreatedAtAction(nameof(GetById), new { id = createdSchedule.Id}, createdSchedule);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<BarberScheduleResponseDto>> Update([FromBody] BarberScheduleUpdateDto schedule, int id)
        {
            var updatedSchedule = await _barberScheduleService.UpdateAsync(id, schedule);

            if (updatedSchedule is null)
            {
                return NotFound(new { message = $"No se pudo actualizar. El horario con ID {id} no existe." });
            }
            
            return Ok(updatedSchedule);

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var delete = await _barberScheduleService.DeleteAsync(id);

            if (!delete)
            {
                return NotFound(new { message = $"No se pudo borrar. El horario con ID {id} no existe." });
            }

            return NoContent();
        }
    }
}
