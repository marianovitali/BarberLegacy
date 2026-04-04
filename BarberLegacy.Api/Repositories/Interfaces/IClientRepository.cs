using BarberLegacy.Api.Entities;

namespace BarberLegacy.Api.Repositories.Interfaces
{
    public interface IClientRepository
    {
        Task<(IEnumerable<Client> Clients, int TotalCount)> GetAllAsync(int pageNumber, int pageSize);
        Task<Client?> GetByIdAsync(int id);
        Task<Client> AddAsync(Client client);
        Task UpdateAsync(Client client);
        Task SoftDeleteAsync(Client client);


    }
}
