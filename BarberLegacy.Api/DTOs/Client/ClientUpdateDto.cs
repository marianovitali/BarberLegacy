using System.ComponentModel.DataAnnotations;

namespace BarberLegacy.Api.DTOs.Client
{
    public class ClientUpdateDto
    {
        [Required]
        public bool IsActive { get; set; }
    }
}
