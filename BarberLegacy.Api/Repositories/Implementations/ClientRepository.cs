using BarberLegacy.Api.Data;
using BarberLegacy.Api.Entities;
using BarberLegacy.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BarberLegacy.Api.Repositories.Implementations
{
    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationDbContext _context;

        public ClientRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Client> AddAsync(Client client)
        {
            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();

            return client;
        }

        public async Task<(IEnumerable<Client> Clients, int TotalCount)> GetAllAsync(int pageNumber, int pageSize)
        {
            var totalCount = await _context.Clients.CountAsync(x => x.IsActive);

            var clients = await _context.Clients
                    .Where(x => x.IsActive)
                    .Include(x => x.User)
                    .Skip((pageNumber - 1) * pageSize) 
                    .Take(pageSize)                    
                    .ToListAsync();

            return (clients, totalCount);
        }

        public async Task<Client?> GetByIdAsync(int id)
        {
            var client = await _context.Clients
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);

            return client;
        }

        public async Task SoftDeleteAsync(Client client)
        {
            client.IsActive = false;
            _context.Clients.Update(client);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Client client)
        {
            _context.Clients.Update(client);
            await _context.SaveChangesAsync();
        }
    }
}
