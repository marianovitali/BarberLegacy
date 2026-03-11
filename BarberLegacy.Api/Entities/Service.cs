using System.ComponentModel.DataAnnotations;

namespace BarberLegacy.Api.Entities
{
    public class Service
    {
        public int Id { get; set; } // PK
        [MaxLength(100)]
        public required string Name { get; set; }
        [MaxLength(500)]
        public required string Description { get; set; }
        [Range(1, 480)]
        public int DurationMinutes { get; set; }
        [Range(0.01, 500000.00)]
        public decimal Price { get; set; }
        public bool IsActive { get; set; } = true;

        // Navigation properties
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
