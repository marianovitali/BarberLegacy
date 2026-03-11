using System.ComponentModel.DataAnnotations;

namespace BarberLegacy.Api.Entities
{
    public class BarberSchedule
    {
        public int Id { get; set; } // PK
        public int BarberId { get; set; } // FK

        [MaxLength(20)]
        public required string DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        // Navigation properties
        public Barber Barber { get; set; } = null!;
    }
}
