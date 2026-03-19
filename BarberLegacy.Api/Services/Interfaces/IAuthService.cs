using BarberLegacy.Api.DTOs.Auth;
using Microsoft.AspNetCore.Identity;

namespace BarberLegacy.Api.Services.Interfaces
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterAsync(RegisterDto dto);

        Task<AuthResponseDto?> LoginAsync(LoginDto dto);
    }
}
