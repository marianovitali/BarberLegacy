using BarberLegacy.Api.Data;
using BarberLegacy.Api.DTOs.Auth;
using BarberLegacy.Api.Entities;
using BarberLegacy.Api.Repositories.Interfaces;
using BarberLegacy.Api.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BarberLegacy.Api.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IClientRepository _clientRepository;
        private readonly ApplicationDbContext _context;
        public AuthService(UserManager<User> userManager, IConfiguration configuration,
            IClientRepository clientRepository, ApplicationDbContext context)
        {
            _userManager = userManager;
            _configuration = configuration;
            _clientRepository = clientRepository;
            _context = context;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterDto dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var user = new User
                {
                    UserName = dto.Email,
                    Email = dto.Email,
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,

                };

                var result = await _userManager.CreateAsync(user, dto.Password);

                if (result.Succeeded)
                {
                    var client = new Client
                    {
                        UserId = user.Id,
                        CreatedAt = DateTime.UtcNow,
                        IsActive = true
                    };

                    await _clientRepository.AddAsync(client);
                    await transaction.CommitAsync();
                }

                return result;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
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
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id), 
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), 

        };


            var secretKey = _configuration["Jwt:Key"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
