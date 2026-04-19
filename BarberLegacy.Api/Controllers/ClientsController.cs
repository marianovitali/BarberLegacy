using BarberLegacy.Api.DTOs.Client;
using BarberLegacy.Api.Helpers;
using BarberLegacy.Api.Repositories.Interfaces;
using BarberLegacy.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BarberLegacy.Api.Controllers
{
    [ApiController]
    [Route("api/clients")]
    [Authorize]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly IClientRepository _clientRepository;

        public ClientsController(IClientService clientService, IClientRepository clientRepository)
        {
            _clientService = clientService;
            _clientRepository = clientRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Barber")]
        [EndpointSummary("Obtiene todos los clientes paginados")]
        [EndpointDescription("Solo administradores o barberos pueden acceder a la lista completa de clientes.")]
        [ProducesResponseType(typeof(PagedResponse<ClientResponseDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResponse<ClientResponseDto>>> GetAll([FromQuery] PaginationParams paginationParams)
        {
            var pagedClients = await _clientService.GetAllAsync(paginationParams);

            return Ok(pagedClients);
        }

        [HttpGet("{id:int}")]
        [EndpointSummary("Obtiene un cliente por ID")]
        [EndpointDescription("Un cliente solo puede ver su propia información, mientras que administradores y barberos pueden ver cualquier cliente.")]
        [ProducesResponseType(typeof(ClientResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ClientResponseDto>> GetById(int id)
        {
            if (!User.IsInRole("Admin") && !User.IsInRole("Barber"))
            {
                if (!await IsUserOwnerAsync(id))
                {
                    return Forbid();
                }
            }

            var client = await _clientService.GetByIdAsync(id);
            if (client is null)
            {
                return NotFound(new { message = $"El cliente con ID {id} no fue encontrado." });
            }

            return Ok(client);
        }

        [HttpPost]
        [AllowAnonymous]
        [EndpointSummary("Crea un nuevo cliente")]
        [EndpointDescription("Permite registrar un nuevo cliente en el sistema sin autenticación.")]
        [ProducesResponseType(typeof(ClientResponseDto), StatusCodes.Status201Created)]
        public async Task<ActionResult<ClientResponseDto>> Create([FromBody] ClientCreateDto client)
        {
            var createdClient = await _clientService.CreateAsync(client);

            return CreatedAtAction(nameof(GetById), new { id = createdClient.Id }, createdClient);
        }

        [HttpPut("{id:int}")]
        [EndpointSummary("Actualiza un cliente existente")]
        [EndpointDescription("Un cliente puede actualizar su propia información. Los administradores pueden actualizar cualquier cliente.")]
        [ProducesResponseType(typeof(ClientResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ClientResponseDto>> Update(int id, [FromBody] ClientUpdateDto client)
        {
            if (!User.IsInRole("Admin"))
            {
                if (!await IsUserOwnerAsync(id))
                {
                    return Forbid();
                }
            }

            var updatedClient = await _clientService.UpdateAsync(id, client);

            if (updatedClient is null)
            {
                return NotFound(new { message = $"No se pudo actualizar. El cliente con ID {id} no existe." });
            }

            return Ok(updatedClient);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin, Barber")]
        [EndpointSummary("Elimina un cliente existente")]
        [EndpointDescription("Solo administradores o barberos pueden eliminar clientes.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var delete = await _clientService.DeleteAsync(id);

            if (!delete)
            {
                return NotFound(new { message = $"No se pudo borrar. El cliente con ID {id} no existe." });
            }

            return NoContent();
        }

        private async Task<bool> IsUserOwnerAsync(int clientId)
        {
            var loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(loggedInUserId)) return false;

            var client = await _clientRepository.GetByIdAsync(clientId);
            return client != null && client.UserId == loggedInUserId;
        }
    }
}