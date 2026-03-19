using BarberLegacy.Api.DTOs.Client;
using BarberLegacy.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace BarberLegacy.Api.Controllers
{
    [ApiController]
    [Route("api/clients")]

    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<IEnumerable<ClientResponseDto>> GetAll()
        {
            var clients = await _clientService.GetAllAsync();
            return clients;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ClientResponseDto>> GetById(int id)
        {
            var client = await _clientService.GetByIdAsync(id);
            if (client is null)
            {
                return NotFound(new { message = $"El cliente con ID {id} no fue encontrado." });
            }

            return Ok(client);
        }

        [HttpPost]
        public async Task<ActionResult<ClientResponseDto>> Create([FromBody] ClientCreateDto client)
        {
            var createdClient = await _clientService.CreateAsync(client);

            return CreatedAtAction(nameof(GetById), new { id = createdClient.Id }, createdClient);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ClientResponseDto>> Update(int id, [FromBody] ClientUpdateDto client)
        {
            var updatedClient = await _clientService.UpdateAsync(id, client);

            if (updatedClient is null)
            {
                return NotFound(new { message = $"No se pudo actualizar. El cliente con ID {id} no existe." });
            }

            return Ok(updatedClient);

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var delete = await _clientService.DeleteAsync(id);

            if (!delete)
            {
                return NotFound(new { message = $"No se pudo borrar. El cliente con ID {id} no existe." });
            }

            return NoContent();
        }
    }
}
