using System.ComponentModel.DataAnnotations;

namespace BarberLegacy.Api.DTOs.BarberShop
{
    public class BarberShopCreateDto
    {

        [MaxLength(200)]
        [Required(ErrorMessage = "El nombre de la barberia es obligatorio.")]
        public required string Name { get; set; }

        [MaxLength(500)]
        [Required(ErrorMessage = "La direccion de la barberia es obligatoria.")]

        public required string Address { get; set; }

        [MaxLength(20)]
        [Phone(ErrorMessage = "El formato del teléfono no es válido.")]
        public string? PhoneNumber { get; set; }

        [MaxLength(255)]
        [EmailAddress]
        public string? Email { get; set; }

    }
}
