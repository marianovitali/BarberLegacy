using BarberLegacy.Api.DTOs.Appointment;
using BarberLegacy.Api.Entities;
using BarberLegacy.Api.Repositories.Interfaces;
using BarberLegacy.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BarberLegacy.Api.Controllers
{
    [ApiController]
    [Route("api/appointments")]
    [Authorize]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IClientRepository _clientRepository;

        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentResponseDto>> GetAppointment(int id)
        {
            var appointment = await _appointmentService.GetByIdAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            return Ok(appointment);
        }

        [HttpGet("client/{clientId}")]
        public async Task<ActionResult<IEnumerable<AppointmentResponseDto>>> GetAppointmentsByClient(int clientId)
        {
            if (!await IsUserOwnerOfClientAsync(clientId))
            {
                return Forbid();
            }

            var appointments = await _appointmentService.GetAllClientAsync(clientId);
            return Ok(appointments);
        }

        [HttpGet("barber/{barberId}")]
        public async Task<ActionResult<IEnumerable<AppointmentResponseDto>>> GetAppointmentsByBarber(int barberId)
        {
            var appointments = await _appointmentService.GetAllBarberAsync(barberId);
            return Ok(appointments);
        }

        [HttpGet("barber/{barberId}/date/{date}")]
        public async Task<ActionResult<IEnumerable<AppointmentResponseDto>>> GetAppointmentByBarberAndDate(int barberId, DateTime date)
        {
            var appointments = await _appointmentService.GetAllBarberByDateAsync(barberId, date);
            return Ok(appointments);
        }

        [HttpPost]
        public async Task<ActionResult<AppointmentResponseDto>> Create(AppointmentCreateDto dto)
        {
            var appointment = await _appointmentService.CreateAsync(dto);

            if (appointment == null)
            {
                return BadRequest("No se pudo agendar el turno. Revisá que el local esté abierto y que el horario no esté ocupado.");
            }

            return Ok(appointment);
        }

        [Authorize(Roles = "Admin, Barber")]
        [HttpPut("{id}")]
        public async Task<ActionResult<AppointmentResponseDto>> UpdateAppointment(int id, AppointmentUpdateDto dto)
        {
            var updatedAppointment = await _appointmentService.UpdateAsync(dto, id);

            if (updatedAppointment == null)
            {
                return BadRequest("No se pudo agendar el turno. Revisá que el local esté abierto y que el horario no esté ocupado.");
            }

            return Ok(updatedAppointment);
        }

        [Authorize(Roles = "Admin, Barber")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var delete = await _appointmentService.DeleteAsync(id);

            if (!delete)
            {
                return NotFound(new { message = $"No se pudo borrar. El appointment con ID {id} no existe." });
            }

            return NoContent();
        }

        private async Task<bool> IsUserOwnerOfClientAsync(int clientId)
        {
            // 1. Sacamos el DNI del token del usuario que hizo la petición
            var loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Si por algún motivo no hay token (no debería pasar por el [Authorize], pero por las dudas)
            if (string.IsNullOrEmpty(loggedInUserId)) return false;

            // 2. Buscamos el cliente en la base de datos
            var client = await _clientRepository.GetByIdAsync(clientId);

            // 3. Devolvemos true si existe y si los IDs coinciden. Si no, false.
            return client != null && client.UserId == loggedInUserId;
        }
    }
}
