using System.ComponentModel.DataAnnotations;

namespace BarberLegacy.Api.DTOs.Barber
{
    public class BarberResponseDto
    {
        public int Id { get; set; } // PK
        public required string UserId { get; set; } // FK
        public int BarberShopId { get; set; } // FK
        [MaxLength(500)]
        public string? Bio { get; set; }
        [MaxLength(500)]
        public string? PhotoUrl { get; set; }
        public bool IsActive { get; set; } = true;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
    }
}
