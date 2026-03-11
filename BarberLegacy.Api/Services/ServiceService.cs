using AutoMapper;
using BarberLegacy.Api.DTOs.Services;
using BarberLegacy.Api.Entities;
using BarberLegacy.Api.Repositories;

namespace BarberLegacy.Api.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _repository;
        private readonly IMapper _mapper;

        public ServiceService(IServiceRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ServiceResponseDto> CreateAsync(ServiceCreateDto dto)
        {
            var serviceEntity = _mapper.Map<Service>(dto);

            serviceEntity.IsActive = true;

            var savedService = await _repository.AddAsync(serviceEntity);

            return _mapper.Map<ServiceResponseDto>(savedService);

        }

        public async Task<bool> DeleteAsync(int id)
        {
            var service = await _repository.GetByIdAsync(id);
            if (service == null)
            {
                return false;
            }

            await _repository.SoftDeleteAsync(service);
            return true;
        }

        public async Task<IEnumerable<ServiceResponseDto>> GetAllAsync()
        {
            var services = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ServiceResponseDto>>(services);
        }

        public async Task<ServiceResponseDto?> GetByIdAsync(int id)
        {
            var service = await _repository.GetByIdAsync(id);

            if (service == null) return null;


            return _mapper.Map<ServiceResponseDto>(service);
        }

        public async Task<ServiceResponseDto?> UpdateAsync(int id, ServiceUpdateDto dto)
        {
            var existingService = await _repository.GetByIdAsync(id);
            if (existingService == null)
            {
                return null;
            }
            _mapper.Map(dto, existingService);

            await _repository.UpdateAsync(existingService);

            return _mapper.Map<ServiceResponseDto>(existingService);
        }
    }
}
