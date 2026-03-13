using System.ComponentModel.DataAnnotations;

namespace BarberLegacy.Api.DTOs.Auth
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [MaxLength(100)]
        public required string FirstName { get; set; }
        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [MaxLength(100)]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "El email es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del email no es válido.")]
        public string Email { get; set; } = default!;

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        public string Password { get; set; } = default!;

        [Required(ErrorMessage = "Debes confirmar la contraseña.")]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmPassword { get; set; } = default!;
    }
}
