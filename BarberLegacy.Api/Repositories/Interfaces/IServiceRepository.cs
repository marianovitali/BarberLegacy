using BarberLegacy.Api.Data;
using BarberLegacy.Api.Entities;

namespace BarberLegacy.Api.Repositories.Interfaces
{
    public interface IServiceRepository
    {
        Task<IEnumerable<Service>> GetAllAsync();

        Task<Service?> GetByIdAsync(int id);

        Task<Service> AddAsync(Service service);

        Task UpdateAsync(Service service);

        Task SoftDeleteAsync(Service service);
    }
}
