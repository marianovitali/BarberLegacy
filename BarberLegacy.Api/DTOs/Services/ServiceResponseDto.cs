using System.ComponentModel.DataAnnotations;

namespace BarberLegacy.Api.DTOs.Services
{
    public class ServiceResponseDto
    {
        public int Id { get; set; } // PK
        public required string Name { get; set; }
        public required string Description { get; set; }
        public int DurationMinutes { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
