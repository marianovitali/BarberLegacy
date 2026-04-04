using System.ComponentModel.DataAnnotations;

namespace BarberLegacy.Api.DTOs.BarberSchedule
{
    public class BarberScheduleCreateDto
    {
        [Required(ErrorMessage = "El ID del barbero es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del barbero debe ser mayor a 0.")]
        public required int BarberId { get; set; }

        [Required(ErrorMessage = "El día de la semana es obligatorio.")]
        public required DayOfWeek DayOfWeek { get; set; }

        [Required(ErrorMessage = "La hora de inicio es obligatoria.")]
        public required TimeSpan StartTime { get; set; }

        [Required(ErrorMessage = "La hora de fin es obligatoria.")]
        public required TimeSpan EndTime { get; set; }
    }
}
