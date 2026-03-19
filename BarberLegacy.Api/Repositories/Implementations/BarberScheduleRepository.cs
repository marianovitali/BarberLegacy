using BarberLegacy.Api.Data;
using BarberLegacy.Api.Entities;
using BarberLegacy.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BarberLegacy.Api.Repositories.Implementations
{
    public class BarberScheduleRepository : IBarberScheduleRepository
    {
        private readonly ApplicationDbContext _context;

        public BarberScheduleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BarberSchedule> AddAsync(BarberSchedule schedule)
        {
            await _context.BarberSchedules.AddAsync(schedule);
            await _context.SaveChangesAsync();

            return schedule;
        }

        public async Task<IEnumerable<BarberSchedule>> GetAllAsync()
        {
            var schedules = await _context.BarberSchedules
                .Where(x => x.IsActive)
                .ToListAsync();

            return schedules;
        }

        public async Task<IEnumerable<BarberSchedule>> GetByBarberIdAsync(int barberId)
        {
            return await _context.BarberSchedules
                    .Where(x => x.IsActive && x.BarberId == barberId)
                    .ToListAsync();
        }

        public async Task<BarberSchedule?> GetByIdAsync(int id)
        {
            var schedule = await _context.BarberSchedules
                .FirstOrDefaultAsync(x => x.Id == id);

            return schedule;
        }

        public async Task SoftDeleteAsync(BarberSchedule schedule)
        {
            schedule.IsActive = false;
            _context.BarberSchedules.Update(schedule);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(BarberSchedule schedule)
        {
            _context.BarberSchedules.Update(schedule);

            await _context.SaveChangesAsync();
        }
    }
}
