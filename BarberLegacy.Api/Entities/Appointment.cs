using BarberLegacy.Api.Enums;
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
        public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; } = true;

        // Navigation properties
        public Client Client { get; set; } = null!;
        public Barber Barber { get; set; } = null!;
        public Service Service { get; set; } = null!;
    }
}
