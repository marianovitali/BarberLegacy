namespace BarberLegacy.Api.DTOs.Auth
{
    public class AuthResponseDto
    {
        public string Token { get; set; } = default!;
        public DateTime Expiration { get; set; }
        public string Email { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
    }
}
