using BarberLegacy.Api.DTOs.Auth;
using BarberLegacy.Api.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BarberLegacy.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterDto dto)
        {
            var user = new User
            {
                UserName = dto.Email,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,

            };

            return await _userManager.CreateAsync(user, dto.Password);
        }

        public async Task<AuthResponseDto?> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
            {
                return null;
            }

            var expirationDate = DateTime.UtcNow.AddHours(2);
            var token = GenerateJwtToken(user, expirationDate);

            return new AuthResponseDto
            {
                Token = token,
                Expiration = expirationDate,
                Email = user.Email!,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }

        private string GenerateJwtToken(User user, DateTime expiration)
        {
            // A. Los "Claims" (Afirmaciones): Es la información pública que viaja dentro del token
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id), // El ID del usuario
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // Un ID único para este token
            // Acá en el futuro podés agregar roles: new Claim(ClaimTypes.Role, "Admin")
        };

            // B. La Firma Digital: Traemos la clave secreta de tu appsettings.json
            var secretKey = _configuration["Jwt:Key"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // C. El armado del Token
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: creds
            );

            // D. Lo convertimos a string (el choclazo de texto final)
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
