using System.ComponentModel.DataAnnotations;

namespace BarberLegacy.Api.DTOs.Barber
{
    public class BarberResponseDto
    {
        public int Id { get; set; } // PK
        public required string UserId { get; set; } // FK
        public int BarberShopId { get; set; } // FK
        public string? Bio { get; set; }
        public string? PhotoUrl { get; set; }
        public bool IsActive { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
    }
}
