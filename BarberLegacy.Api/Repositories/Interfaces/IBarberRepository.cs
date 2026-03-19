using BarberLegacy.Api.Entities;

namespace BarberLegacy.Api.Repositories.Interfaces
{
    public interface IBarberRepository
    {
        Task<IEnumerable<Barber>> GetAllAsync();
        Task<Barber?> GetByIdAsync(int id);
        Task<Barber> AddAsync(Barber barber);
        Task UpdateAsync(Barber barber);
        Task SoftDeleteAsync(Barber barber);

    }
}
