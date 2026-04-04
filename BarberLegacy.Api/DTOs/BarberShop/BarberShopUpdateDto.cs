using System.ComponentModel.DataAnnotations;

namespace BarberLegacy.Api.DTOs.BarberShop
{
    public class BarberShopUpdateDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [MaxLength(200, ErrorMessage = "El nombre no puede superar los 200 caracteres.")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "La dirección es obligatoria.")]
        [MaxLength(500, ErrorMessage = "La dirección no puede superar los 500 caracteres.")]
        public required string Address { get; set; }

        [MaxLength(20, ErrorMessage = "El teléfono no puede superar los 20 caracteres.")]
        [Phone(ErrorMessage = "El formato del teléfono no es válido.")]
        public string? PhoneNumber { get; set; }

        [MaxLength(255, ErrorMessage = "El email no puede superar los 255 caracteres.")]
        [EmailAddress(ErrorMessage = "El formato del email no es válido.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio.")]
        public bool IsActive { get; set; }
    }
}
