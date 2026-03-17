using System.ComponentModel.DataAnnotations;

namespace BarberLegacy.Api.DTOs.Barber
{
    public class BarberCreateDto
    {
        [Required(ErrorMessage = "El ID del usuario es obligatorio.")]
        public required string UserId { get; set; } = default!;

        [Required(ErrorMessage = "El ID de la sucursal es obligatorio.")]
        public int BarberShopId { get; set; } // FK
        [MaxLength(500, ErrorMessage = "La biografía no puede superar los 500 caracteres.")]
        public string? Bio { get; set; }
        [MaxLength(500)]
        public string? PhotoUrl { get; set; }
    }
}
