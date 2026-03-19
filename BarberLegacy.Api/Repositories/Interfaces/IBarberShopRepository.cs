using BarberLegacy.Api.Entities;

namespace BarberLegacy.Api.Repositories.Interfaces
{
    public interface IBarberShopRepository
    {
        Task<IEnumerable<BarberShop>> GetAllAsync();
        Task<BarberShop?> GetByIdAsync(int id);
        Task<BarberShop> AddAsync(BarberShop barberShop);
        Task UpdateAsync(BarberShop barberShop);
        Task SoftDeleteAsync(BarberShop barberShop);

    }
}
