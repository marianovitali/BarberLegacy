using AutoMapper;
using BarberLegacy.Api.DTOs.BarberSchedule;
using BarberLegacy.Api.DTOs.Services;
using BarberLegacy.Api.Entities;
using BarberLegacy.Api.Repositories.Interfaces;
using BarberLegacy.Api.Services.Interfaces;

namespace BarberLegacy.Api.Services.Implementations
{
    public class BarberScheduleService : IBarberScheduleService
    {
        private readonly IBarberScheduleRepository _repository;
        private readonly IMapper _mapper;

        public BarberScheduleService(IBarberScheduleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BarberScheduleResponseDto> CreateAsync(BarberScheduleCreateDto dto)
        {
            var scheduleEntity = _mapper.Map<BarberSchedule>(dto);
            scheduleEntity.IsActive = true;

            var savedSchedule = await _repository.AddAsync(scheduleEntity);

            return _mapper.Map<BarberScheduleResponseDto>(savedSchedule);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var schedule = await _repository.GetByIdAsync(id);

            if (schedule == null)
            {
                return false;
            }

            await _repository.SoftDeleteAsync(schedule);

            return true; 
        }

        public async Task<IEnumerable<BarberScheduleResponseDto>> GetAllAsync()
        {
            var schedules = await _repository.GetAllAsync();

            return _mapper.Map<IEnumerable<BarberScheduleResponseDto>>(schedules);
        }

        public async Task<IEnumerable<BarberScheduleResponseDto>> GetByBarberIdAsync(int barberId)
        {
            var schedules = await _repository.GetByBarberIdAsync(barberId);

            return _mapper.Map<IEnumerable<BarberScheduleResponseDto>>(schedules);
        }

        public async Task<BarberScheduleResponseDto?> GetByIdAsync(int id)
        {
            var schedule = await _repository.GetByIdAsync(id);

            if (schedule == null)
            {
                return null;
            }

            return _mapper.Map<BarberScheduleResponseDto>(schedule);
        }

        public async Task<BarberScheduleResponseDto?> UpdateAsync(int id, BarberScheduleUpdateDto dto)
        {
            var existingSchedule = await _repository.GetByIdAsync(id);

            if (existingSchedule == null)
            {
                return null;
            }

            _mapper.Map(dto, existingSchedule);

            await _repository.UpdateAsync(existingSchedule);

            return _mapper.Map<BarberScheduleResponseDto>(existingSchedule);


        }
    }
}
