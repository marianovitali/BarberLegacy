using System.ComponentModel.DataAnnotations;

namespace BarberLegacy.Api.Entities
{
    public class Client
    {
        public int Id { get; set; } // PK
        [MaxLength(200)]
        public required string UserId { get; set; } // FK
        public DateTime CreatedAt { get; set; }

        public User User { get; set; } = null!;
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
