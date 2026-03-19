using System.ComponentModel.DataAnnotations;

namespace BarberLegacy.Api.DTOs.BarberSchedule
{
    public class BarberScheduleCreateDto
    {
        public required int BarberId { get; set; }
        public required DayOfWeek DayOfWeek { get; set; }
        public required TimeSpan StartTime { get; set; }
        public required TimeSpan EndTime { get; set; }
    }
}
