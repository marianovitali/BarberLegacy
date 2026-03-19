using BarberLegacy.Api.Data;
using BarberLegacy.Api.Entities;
using BarberLegacy.Api.Repositories.Interfaces;
using BarberLegacy.Api.Services;
using Microsoft.EntityFrameworkCore;

namespace BarberLegacy.Api.Repositories.Implementations
{
    public class BarberShopRepository : IBarberShopRepository
    {
        private readonly ApplicationDbContext _context;

        public BarberShopRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BarberShop> AddAsync(BarberShop barberShop)
        {
            await _context.BarberShops.AddAsync(barberShop);
            await _context.SaveChangesAsync();

            return barberShop;
        }

        public async Task<IEnumerable<BarberShop>> GetAllAsync()
        {
            return await _context.BarberShops
                .Where(x => x.IsActive)
                .ToListAsync();
        }

        public async Task<BarberShop?> GetByIdAsync(int id)
        {
            return await _context.BarberShops
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task SoftDeleteAsync(BarberShop barberShop)
        {
            barberShop.IsActive = false;
            _context.BarberShops.Update(barberShop);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(BarberShop barberShop)
        {
            _context.BarberShops.Update(barberShop);

            await _context.SaveChangesAsync();
        }
    }
}
