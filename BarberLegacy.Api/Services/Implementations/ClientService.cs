using AutoMapper;
using BarberLegacy.Api.DTOs.Client;
using BarberLegacy.Api.Entities;
using BarberLegacy.Api.Repositories.Interfaces;
using BarberLegacy.Api.Services.Interfaces;

namespace BarberLegacy.Api.Services.Implementations
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _repository;
        private readonly IMapper _mapper;

        public ClientService(IClientRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ClientResponseDto> CreateAsync(ClientCreateDto dto)
        {
            var clientEntity = _mapper.Map<Client>(dto);

            clientEntity.IsActive = true;

            var savedClient = await _repository.AddAsync(clientEntity);

            var completeClient = await _repository.GetByIdAsync(savedClient.Id);

            return _mapper.Map<ClientResponseDto>(completeClient!);

        }

        public async Task<bool> DeleteAsync(int id)
        {
            var client = await _repository.GetByIdAsync(id);
            if (client == null)
            {
                return false;
            }

            await _repository.SoftDeleteAsync(client);
            return true;
        }

        public async Task<IEnumerable<ClientResponseDto>> GetAllAsync()
        {
            var clients = await _repository.GetAllAsync();

            return _mapper.Map<IEnumerable<ClientResponseDto>>(clients);
        }

        public async Task<ClientResponseDto?> GetByIdAsync(int id)
        {
            var client = await _repository.GetByIdAsync(id);

            if (client == null) return null;

            return _mapper.Map<ClientResponseDto>(client);
        }

        public async Task<ClientResponseDto?> UpdateAsync(int id, ClientUpdateDto dto)
        {
            var existingClient = await _repository.GetByIdAsync(id);

            if (existingClient == null) return null;

            _mapper.Map(dto, existingClient);

            await _repository.UpdateAsync(existingClient);

            return _mapper.Map<ClientResponseDto>(existingClient);
        }

    }
}
