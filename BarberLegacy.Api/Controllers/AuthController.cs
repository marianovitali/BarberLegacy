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
        [EndpointSummary("Registra un nuevo usuario")]
        [EndpointDescription("Crea una cuenta de usuario en el sistema. Devuelve un mensaje de éxito o errores de validación.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        [EndpointSummary("Inicia sesión de usuario")]
        [EndpointDescription("Valida las credenciales y devuelve un token JWT si son correctas.")]
        [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
