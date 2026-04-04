using BarberLegacy.Api.DTOs.Appointment;

namespace BarberLegacy.Api.Services.Interfaces
{
    public interface IAppointmentService
    {
        Task<IEnumerable<AppointmentResponseDto>> GetAllClientAsync(int clientId);
        Task<IEnumerable<AppointmentResponseDto>> GetAllBarberAsync(int barberId);
        Task<IEnumerable<AppointmentResponseDto>> GetAllBarberByDateAsync(int barberId, DateTime date);
        Task<IEnumerable<TimeSpan>> GetAvailableSlotsAsync(int barberId, DateTime date);
        Task<AppointmentResponseDto?> GetByIdAsync(int id);
        Task<AppointmentResponseDto> CreateAsync(AppointmentCreateDto dto);
        Task<AppointmentResponseDto?> UpdateAsync(AppointmentUpdateDto dto, int id);
        Task<bool> DeleteAsync(int id);
    }
}
