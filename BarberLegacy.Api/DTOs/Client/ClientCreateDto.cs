using BarberLegacy.Api.Entities;
using System.ComponentModel.DataAnnotations;

namespace BarberLegacy.Api.DTOs.Client
{
    public class ClientCreateDto
    {
        [Required(ErrorMessage = "El ID del usuario es obligatorio.")]
        public required string UserId { get; set; }
    }
}
