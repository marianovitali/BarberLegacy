using System.ComponentModel.DataAnnotations;

namespace BarberLegacy.Api.Entities
{
    public class BarberShop
    {
        public int Id { get; set; } // PK

        [MaxLength(200)]
        public required string Name { get; set; }

        [MaxLength(500)]
        public required string Address { get; set; }

        [MaxLength(20)]
        public string? PhoneNumber { get; set; }

        [MaxLength(255)]
        [EmailAddress]
        public string? Email { get; set; }

        public bool IsActive { get; set; } = true;

        // Navigation properties
        public ICollection<Barber> Barbers { get; set; } = new List<Barber>();
    }
}
