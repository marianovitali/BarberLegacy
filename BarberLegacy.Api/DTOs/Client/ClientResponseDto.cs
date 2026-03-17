namespace BarberLegacy.Api.DTOs.Client
{
    public class ClientResponseDto
    {
        public int Id { get; set; }
        public required string UserId { get; set; } 
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }

        // IDENTITY
        public required string FirstName { get; set; }
        public required string LastName { get; set; } 
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
