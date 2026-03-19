using AutoMapper;
using BarberLegacy.Api.DTOs.BarberShop;
using BarberLegacy.Api.Entities;
using BarberLegacy.Api.Repositories.Interfaces;
using BarberLegacy.Api.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BarberLegacy.Api.Services.Implementations
{
    public class BarberShopService : IBarberShopService
    {
        private readonly IBarberShopRepository _repository;
        private readonly IMapper _mapper;

        public BarberShopService(IBarberShopRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BarberShopResponseDto> CreateAsync(BarberShopCreateDto dto)
        {
            var barberShopEntity = _mapper.Map<BarberShop>(dto);

            barberShopEntity.IsActive = true;

            var savedBarberShop = await _repository.AddAsync(barberShopEntity);
            
            return _mapper.Map<BarberShopResponseDto>(savedBarberShop);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var barberShop = await _repository.GetByIdAsync(id);

            if (barberShop == null)
            {
                return false;
            }

            await _repository.SoftDeleteAsync(barberShop);

            return true;
        }

        public async Task<IEnumerable<BarberShopResponseDto>> GetAllAsync()
        {
            var barberShops = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<BarberShopResponseDto>>(barberShops);
        }

        public async Task<BarberShopResponseDto?> GetByIdAsync(int id)
        {
            var barberShop = await _repository.GetByIdAsync(id);

            if (barberShop == null) return null;

            return _mapper.Map<BarberShopResponseDto>(barberShop);
        }

        public async Task<BarberShopResponseDto?> UpdateAsync(int id, BarberShopUpdateDto dto)
        {
            var existingBarberShop = await _repository.GetByIdAsync(id);

            if (existingBarberShop == null)
            {
                return null;
            }

            _mapper.Map(dto, existingBarberShop);

            await _repository.UpdateAsync(existingBarberShop);

            return _mapper.Map<BarberShopResponseDto>(existingBarberShop);
        }
    }
}
