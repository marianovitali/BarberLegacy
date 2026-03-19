using System.ComponentModel.DataAnnotations;

namespace BarberLegacy.Api.DTOs.BarberShop
{
    public class BarberShopUpdateDto
    {
        public required string Name { get; set; }
        public required string Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
