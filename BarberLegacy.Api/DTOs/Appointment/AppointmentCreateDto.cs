using System.ComponentModel.DataAnnotations;

namespace BarberLegacy.Api.DTOs.Appointment
{
    public class AppointmentCreateDto
    {
        [Required(ErrorMessage = "El ID del cliente es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del cliente debe ser mayor a 0.")]
        public required int ClientId { get; set; }
        [Required(ErrorMessage = "El ID del barbero es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del barbero debe ser mayor a 0.")]
        public required int BarberId { get; set; }
        [Required(ErrorMessage = "El ID del servicio es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del servicio debe ser mayor a 0.")]
        public required int ServiceId { get; set; }
        [Required(ErrorMessage = "La fecha es obligatoria.")]
        public required DateTime Date { get; set; }
        [Required(ErrorMessage = "La hora de inicio es obligatoria.")]
        public required TimeSpan StartTime { get; set; }
    }
}
