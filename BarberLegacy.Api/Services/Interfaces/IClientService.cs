using BarberLegacy.Api.DTOs.Client;
using BarberLegacy.Api.Entities;

namespace BarberLegacy.Api.Services.Interfaces
{
    public interface IClientService
    {
        Task<IEnumerable<ClientResponseDto>> GetAllAsync();
        Task<ClientResponseDto?> GetByIdAsync(int id);
        Task<ClientResponseDto> CreateAsync(ClientCreateDto dto);
        Task<ClientResponseDto?> UpdateAsync(int id, ClientUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
