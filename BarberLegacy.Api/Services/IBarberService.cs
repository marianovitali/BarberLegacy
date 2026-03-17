using BarberLegacy.Api.DTOs.Barber;
using BarberLegacy.Api.Entities;

namespace BarberLegacy.Api.Services
{
    public interface IBarberService
    {
        Task<IEnumerable<BarberResponseDto>> GetAllAsync();
        Task<BarberResponseDto?> GetByIdAsync(int id);
        Task<BarberResponseDto> CreateAsync(BarberCreateDto barber);
        Task<BarberResponseDto?> UpdateAsync(int id, BarberUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
