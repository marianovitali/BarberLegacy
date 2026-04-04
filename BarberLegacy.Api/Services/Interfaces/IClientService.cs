using BarberLegacy.Api.DTOs.Client;
using BarberLegacy.Api.Entities;
using BarberLegacy.Api.Helpers;

namespace BarberLegacy.Api.Services.Interfaces
{
    public interface IClientService
    {
        Task<PagedResponse<ClientResponseDto>> GetAllAsync(PaginationParams paginationParams);
        Task<ClientResponseDto?> GetByIdAsync(int id);
        Task<ClientResponseDto> CreateAsync(ClientCreateDto dto);
        Task<ClientResponseDto?> UpdateAsync(int id, ClientUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
