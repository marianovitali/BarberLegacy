using BarberLegacy.Api.DTOs.Appointment;
using BarberLegacy.Api.Entities;
using BarberLegacy.Api.Repositories.Implementations;
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
    [Tags("Appointments")]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IClientRepository _clientRepository;

        public AppointmentsController(IAppointmentService appointmentService, IClientRepository clientRepository)
        {
            _appointmentService = appointmentService;
            _clientRepository = clientRepository;
        }
        [HttpGet("{id}")]
        [EndpointSummary("Obtiene un turno específico por su ID")]
        [ProducesResponseType(typeof(AppointmentResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        [EndpointSummary("Obtiene todos los turnos de un cliente")]
        [EndpointDescription("Solo el dueño de la cuenta o un administrador pueden ver estos turnos.")]
        [ProducesResponseType(typeof(IEnumerable<AppointmentResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]

        public async Task<ActionResult<IEnumerable<AppointmentResponseDto>>> GetAppointmentsByClient(int clientId)
        {
            if (!await IsUserOwnerOfClientAsync(clientId))
            {
                return Forbid();
            }

            var appointments = await _appointmentService.GetAllClientAsync(clientId);
            return Ok(appointments);
        }

        [Authorize(Roles = "Admin, Barber")]
        [HttpGet("barber/{barberId}")]
        [EndpointSummary("Obtiene la agenda completa de un barbero")]
        [ProducesResponseType(typeof(IEnumerable<AppointmentResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<AppointmentResponseDto>>> GetAppointmentsByBarber(int barberId)
        {
            var appointments = await _appointmentService.GetAllBarberAsync(barberId);
            return Ok(appointments);
        }

        [HttpGet("barber/{barberId}/date/{date}")]
        [EndpointSummary("Obtiene los turnos de un barbero para una fecha específica")]
        [ProducesResponseType(typeof(IEnumerable<AppointmentResponseDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<AppointmentResponseDto>>> GetAppointmentByBarberAndDate(int barberId, DateTime date)
        {
            var appointments = await _appointmentService.GetAllBarberByDateAsync(barberId, date);
            return Ok(appointments);
        }

        [HttpGet("barber/{barberId}/availability")]
        [AllowAnonymous]
        [EndpointSummary("Consulta los horarios disponibles de un barbero")]
        [EndpointDescription("Devuelve una lista de horas (TimeSpan) en las que el barbero está libre para la fecha indicada.")]
        [ProducesResponseType(typeof(IEnumerable<TimeSpan>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TimeSpan>>> GetAvailableSlots(int barberId, [FromQuery] DateTime date)
        {
            var availableSlots = await _appointmentService.GetAvailableSlotsAsync(barberId, date);

            return Ok(availableSlots);
        }

        [HttpPost]
        [EndpointSummary("Reserva un nuevo turno")]
        [EndpointDescription("Valida que el local esté abierto y que el barbero no tenga otro turno en ese horario.")]
        [ProducesResponseType(typeof(AppointmentResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        [EndpointSummary("Actualiza o reprograma un turno")]
        [ProducesResponseType(typeof(AppointmentResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
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
        [EndpointSummary("Cancela un turno existente")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
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
