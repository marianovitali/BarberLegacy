using BarberLegacy.Api.Data;
using BarberLegacy.Api.Entities;
using BarberLegacy.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BarberLegacy.Api.Repositories.Implementations
{
    public class BarberRepository : IBarberRepository
    {
        private readonly ApplicationDbContext _context;

        public BarberRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Barber> AddAsync(Barber barber)
        {
            await _context.Barbers.AddAsync(barber);
            await _context.SaveChangesAsync();

            return barber;
        }

        public async Task<IEnumerable<Barber>> GetAllAsync()
        {
            var barbers = await _context.Barbers
                .Where(x => x.IsActive)
                .Include(x => x.User)
                .Include(x => x.BarberShop)
                .ToListAsync();

            return barbers;
        }

        public async Task<Barber?> GetByIdAsync(int id)
        {
            var barber = await _context.Barbers
                .Include(x =>x.User)
                .FirstOrDefaultAsync(x => x.Id == id);

            return barber;
        }

        public async Task SoftDeleteAsync(Barber barber)
        {
            barber.IsActive = false;
            _context.Barbers.Update(barber);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Barber barber)
        {
            _context.Barbers.Update(barber);
            await _context.SaveChangesAsync();
        }
    }
}
