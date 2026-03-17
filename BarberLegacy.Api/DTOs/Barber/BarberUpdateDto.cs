using System.ComponentModel.DataAnnotations;

namespace BarberLegacy.Api.DTOs.Barber
{
    public class BarberUpdateDto
    {

        [MaxLength(500, ErrorMessage = "La biografía no puede superar los 500 caracteres.")]
        public string? Bio { get; set; }
        [MaxLength(500)]
        public string? PhotoUrl { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}
