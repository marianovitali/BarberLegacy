using BarberLegacy.Api.Enums;
using System.ComponentModel.DataAnnotations;

namespace BarberLegacy.Api.DTOs.Appointment
{
    public class AppointmentUpdateDto
    {
        [Required(ErrorMessage = "El ID del barbero es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del barbero debe ser mayor a 0.")]
        public int BarberId { get; set; }

        [Required(ErrorMessage = "El ID del servicio es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del servicio debe ser mayor a 0.")]
        public int ServiceId { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria.")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "La hora de inicio es obligatoria.")]
        public TimeSpan StartTime { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio.")]
        public AppointmentStatus Status { get; set; }
    }
}
