using BarberLegacy.Api.Data;
using BarberLegacy.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace BarberLegacy.Api.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly ApplicationDbContext _context;

        public ServiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Service> AddAsync(Service service)
        {
            await _context.Services.AddAsync(service);

            await _context.SaveChangesAsync();

            return service;
        }

        public async Task<IEnumerable<Service>> GetAllAsync()
        {
            return await _context.Services
                .Where(x => x.IsActive == true)
                .ToListAsync();
        }

        public async Task<Service?> GetByIdAsync(int id)
        {
            return await _context.Services
                .FindAsync(id);
        }

        public async Task SoftDeleteAsync(Service service)
        {
            service.IsActive = false;
            _context.Services.Update(service);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Service service)
        {
            _context.Services.Update(service);
            await _context.SaveChangesAsync();
        }
    }
}
