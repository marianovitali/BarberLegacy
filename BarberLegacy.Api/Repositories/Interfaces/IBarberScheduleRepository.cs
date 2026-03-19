using BarberLegacy.Api.Entities;

namespace BarberLegacy.Api.Repositories.Interfaces
{
    public interface IBarberScheduleRepository
    {
        Task<IEnumerable<BarberSchedule>> GetAllAsync();
        Task<BarberSchedule?> GetByIdAsync(int id);
        Task<IEnumerable<BarberSchedule>> GetByBarberIdAsync(int barberId);
        Task<BarberSchedule> AddAsync(BarberSchedule schedule);
        Task UpdateAsync(BarberSchedule schedule);
        Task SoftDeleteAsync(BarberSchedule schedule);
    }
}
