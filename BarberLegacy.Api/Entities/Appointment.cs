using System.ComponentModel.DataAnnotations;

namespace BarberLegacy.Api.Entities
{
    public class Appointment
    {
        public int Id { get; set; } // PK
        public int ClientId { get; set; } // FK
        public int BarberId { get; set; } // FK
        public int ServiceId { get; set; } // FK
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        [MaxLength(50)]
        public required string Status { get; set; } = "Pending"; // Pending, Confirmed, Completed, Cancelled
        public DateTime CreatedAt { get; set; } 

        // Navigation properties
        public Client Client { get; set; } = null!;
        public Barber Barber { get; set; } = null!;
        public Service Service { get; set; } = null!;
    }
}
