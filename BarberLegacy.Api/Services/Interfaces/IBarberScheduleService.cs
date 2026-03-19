using BarberLegacy.Api.DTOs.BarberSchedule;
using BarberLegacy.Api.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BarberLegacy.Api.Services.Interfaces
{
    public interface IBarberScheduleService
    {
        Task<IEnumerable<BarberScheduleResponseDto>> GetAllAsync();
        Task<BarberScheduleResponseDto?> GetByIdAsync(int id);
        Task<IEnumerable<BarberScheduleResponseDto>> GetByBarberIdAsync(int barberId);
        Task<BarberScheduleResponseDto> CreateAsync(BarberScheduleCreateDto dto);
        Task<BarberScheduleResponseDto?> UpdateAsync(int id, BarberScheduleUpdateDto dto);
        Task<bool> DeleteAsync(int id);



    }
}
