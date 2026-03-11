using System.ComponentModel.DataAnnotations;

namespace BarberLegacy.Api.DTOs.Services
{
    public class ServiceUpdateDto
    {
        [Required(ErrorMessage = "El nombre del servicio es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        public required string Name { get; set; } = default!;

        [MaxLength(500, ErrorMessage = "La descripción es demasiado larga.")]
        public required string Description { get; set; }

        [Required]
        [Range(5, 480, ErrorMessage = "La duración debe ser entre 5 y 480 minutos.")]
        public int DurationMinutes { get; set; }

        [Required]
        [Range(0.01, 500000, ErrorMessage = "El precio debe ser mayor a 0.")]
        public decimal Price { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

    }
}
