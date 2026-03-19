using BarberLegacy.Api.DTOs.Auth;
using BarberLegacy.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BarberLegacy.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]

        public async Task<ActionResult<AuthResponseDto>> Register([FromBody] RegisterDto dto)
        {
            var result = await _authService.RegisterAsync(dto);

            if (result.Succeeded)
            {
                return Ok(new { message = "Usuario creado con éxito!" });
            }

            var errors = result.Errors.Select(e => e.Description);
            return BadRequest(new { message = "Error al registrar el usuario", errors });
        }

        [HttpPost("login")]

        public async Task<ActionResult<AuthResponseDto>> Login([FromBody] LoginDto dto)
        {
            var response = await _authService.LoginAsync(dto);

            if (response == null)
            {
                return Unauthorized(new { message = "Credenciales incorrectas. Intente de nuevo." });
            }

            return Ok(response);
        }

    }
}
