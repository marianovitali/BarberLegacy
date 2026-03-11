using AutoMapper;
using BarberLegacy.Api.DTOs.Services;
using BarberLegacy.Api.Entities;

namespace BarberLegacy.Api.Mappings
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {

            // GET
            CreateMap<Service, ServiceResponseDto>();

            // ADD
            CreateMap<ServiceCreateDto, Service>();

            // EDIT
            CreateMap<ServiceUpdateDto, Service>();
        }
    }
}
