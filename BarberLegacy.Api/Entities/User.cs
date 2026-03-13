using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BarberLegacy.Api.Entities
{
    public class User : IdentityUser
    {
        [MaxLength(100)]
        public required string FirstName { get; set; }
        [MaxLength(100)]
        public required string LastName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
        public Client? Client { get; set; }
        public Barber? Barber { get; set; }
    }
}
