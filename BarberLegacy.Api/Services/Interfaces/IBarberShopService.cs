using BarberLegacy.Api.DTOs.BarberShop;

namespace BarberLegacy.Api.Services.Interfaces
{
    public interface IBarberShopService
    {
        Task<IEnumerable<BarberShopResponseDto>> GetAllAsync();
        Task<BarberShopResponseDto?> GetByIdAsync(int id);
        Task<BarberShopResponseDto> CreateAsync(BarberShopCreateDto dto);
        Task<BarberShopResponseDto?> UpdateAsync(int id, BarberShopUpdateDto dto);
        Task<bool> DeleteAsync(int id);



    }
}
