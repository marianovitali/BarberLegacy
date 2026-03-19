using AutoMapper;
using BarberLegacy.Api.DTOs.Barber;
using BarberLegacy.Api.Entities;
using BarberLegacy.Api.Repositories.Interfaces;
using BarberLegacy.Api.Services.Interfaces;

namespace BarberLegacy.Api.Services.Implementations
{
    public class BarberService : IBarberService
    {
        private readonly IBarberRepository _repository;
        private readonly IMapper _mapper;

        public BarberService(IBarberRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BarberResponseDto> CreateAsync(BarberCreateDto dto)
        {
            var barberEntity = _mapper.Map<Barber>(dto);

            barberEntity.IsActive = true;

            var savedBarber = await _repository.AddAsync(barberEntity);

            var completeBarber = await _repository.GetByIdAsync(savedBarber.Id);

            return _mapper.Map<BarberResponseDto>(completeBarber);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var barber = await _repository.GetByIdAsync(id);
            if (barber == null)
            {
                return false;
            }

            await _repository.SoftDeleteAsync(barber);

            return true;
        }

        public async Task<IEnumerable<BarberResponseDto>> GetAllAsync()
        {
            var barbers = await _repository.GetAllAsync();

            return _mapper.Map<IEnumerable<BarberResponseDto>>(barbers);
        }

        public async Task<BarberResponseDto?> GetByIdAsync(int id)
        {
            var barber = await _repository.GetByIdAsync(id);

            if (barber == null)
            {
                return null;
            }

            return _mapper.Map<BarberResponseDto?>(barber);
        }

        public async Task<BarberResponseDto?> UpdateAsync(int id, BarberUpdateDto dto)
        {
            var existingBarber = await _repository.GetByIdAsync(id);

            if (existingBarber == null)
            {
                return null;
            }
            _mapper.Map(dto, existingBarber);

            await _repository.UpdateAsync(existingBarber);

            return _mapper.Map<BarberResponseDto>(existingBarber);

        }
    }
}
