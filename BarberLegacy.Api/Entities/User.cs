using System.ComponentModel.DataAnnotations;

namespace BarberLegacy.Api.Entities
{
    public class User
    {
        [MaxLength(200)]
        public required string Id { get; set; } // PK

        [MaxLength(100)]
        public required string FirstName { get; set; }
        [MaxLength(100)]
        public required string LastName { get; set; }
        [MaxLength(200)]
        [EmailAddress]
        public required string Email { get; set; }
        [MaxLength(20)]
        public string? PhoneNumber { get; set; }
        [MaxLength(50)]
        public required string Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; } = true;
        public Client? Client { get; set; }
        public Barber? Barber { get; set; }
    }
}
