using System.ComponentModel.DataAnnotations;

namespace BarberLegacy.Api.DTOs.BarberSchedule
{
    public class BarberScheduleResponseDto
    {
        public int Id { get; set; } // PK
        public int BarberId { get; set; } // FK
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool IsActive { get; set; }

    }
}
