using System.ComponentModel.DataAnnotations;

namespace BarberLegacy.Api.Entities
{
    public class Barber
    {
        public int Id { get; set; } // PK

        [MaxLength(200)]
        public required string UserId { get; set; } // FK
        public int BarberShopId { get; set; } // FK
        [MaxLength(500)]
        public string? Bio { get; set; }
        [MaxLength(500)]
        public string? PhotoUrl { get; set; }
        public bool IsActive { get; set; } = true;

        // Navigation properties
        public User User { get; set; } = null!;
        public BarberShop BarberShop { get; set; } = null!;
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<BarberSchedule> Schedules { get; set; } = new List<BarberSchedule>();

    }
}
