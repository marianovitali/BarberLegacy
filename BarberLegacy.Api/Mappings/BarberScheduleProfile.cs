using AutoMapper;
using BarberLegacy.Api.DTOs.BarberSchedule;
using BarberLegacy.Api.Entities;

namespace BarberLegacy.Api.Mappings
{
    public class BarberScheduleProfile : Profile
    {
        public BarberScheduleProfile()
        {
            CreateMap<BarberSchedule, BarberScheduleResponseDto>();
            CreateMap<BarberScheduleCreateDto, BarberSchedule>();
            CreateMap<BarberScheduleUpdateDto, BarberSchedule>();
        }
    }
}
