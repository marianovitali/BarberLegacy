using BarberLegacy.Api.DTOs.Client;
using BarberLegacy.Api.Helpers;
using BarberLegacy.Api.Repositories.Interfaces;
using BarberLegacy.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
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
        public async Task<ActionResult<PagedResponse<ClientResponseDto>>> GetAll([FromQuery] PaginationParams paginationParams)
        {
            var pagedClients = await _clientService.GetAllAsync(paginationParams);

            return Ok(pagedClients);
        }

        [HttpGet("{id:int}")]
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
        public async Task<ActionResult<ClientResponseDto>> Create([FromBody] ClientCreateDto client)
        {
            var createdClient = await _clientService.CreateAsync(client);

            return CreatedAtAction(nameof(GetById), new { id = createdClient.Id }, createdClient);
        }

        [HttpPut("{id:int}")]
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
