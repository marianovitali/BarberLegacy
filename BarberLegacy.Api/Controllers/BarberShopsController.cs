using BarberLegacy.Api.DTOs.BarberShop;
using BarberLegacy.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberLegacy.Api.Controllers
{
    [ApiController]
    [Route("api/barbershops")]
    public class BarberShopsController : ControllerBase
    {
        private readonly IBarberShopService _barberShopService;

        public BarberShopsController(IBarberShopService barberShopService)
        {
            _barberShopService = barberShopService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BarberShopResponseDto>>> GetAll()
        {
            var barberShops = await _barberShopService.GetAllAsync();
            return Ok(barberShops);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<BarberShopResponseDto>> GetById(int id)
        {
            var barberShop = await _barberShopService.GetByIdAsync(id);
            if (barberShop is null)
            {
                return NotFound(new { Message = $"La barberia con ID {id} no fue encontrada." });
            }
            return Ok(barberShop);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Barber")]
        public async Task<ActionResult<BarberShopResponseDto>> Create([FromBody] BarberShopCreateDto barberShop)
        {
            var createdBarberShop = await _barberShopService.CreateAsync(barberShop);

            return CreatedAtAction(nameof(GetById), new { id = createdBarberShop.Id }, createdBarberShop);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin, Barber")]
        public async Task<ActionResult<BarberShopResponseDto>> Update([FromBody] BarberShopUpdateDto barberShop, int id)
        {
            var updatedBarberShop = await _barberShopService.UpdateAsync(id, barberShop);

            if (updatedBarberShop == null)
            {
                return NotFound(new { message = $"No se pudo actualizar. La barberia con ID {id} no existe." });
            }

            return Ok(updatedBarberShop);

        }
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin, Barber")]
        public async Task<IActionResult> Delete(int id)
        {
            var delete = await _barberShopService.DeleteAsync(id);

            if (!delete)
            {
                return NotFound(new { message = $"No se pudo borrar. La barberia con ID {id} no existe." });
            }

            return NoContent();
        }
    }
}
