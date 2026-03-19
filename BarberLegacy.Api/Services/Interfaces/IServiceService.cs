using BarberLegacy.Api.DTOs.Services;
using BarberLegacy.Api.Entities;

namespace BarberLegacy.Api.Services.Interfaces
{
    public interface IServiceService
    {
        Task<IEnumerable<ServiceResponseDto>> GetAllAsync();
        Task<ServiceResponseDto?> GetByIdAsync(int id);
        Task<ServiceResponseDto> CreateAsync(ServiceCreateDto dto);
        Task<ServiceResponseDto?> UpdateAsync(int id, ServiceUpdateDto dto);
        Task<bool> DeleteAsync (int id);

    }
}
