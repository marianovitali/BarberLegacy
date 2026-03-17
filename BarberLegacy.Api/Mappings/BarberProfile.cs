using AutoMapper;
using BarberLegacy.Api.DTOs.Barber;
using BarberLegacy.Api.DTOs.Services;
using BarberLegacy.Api.Entities;

namespace BarberLegacy.Api.Mappings
{
    public class BarberProfile : Profile
    {
        public BarberProfile()
        {
            //GET
            CreateMap<Barber, BarberResponseDto>()
                .ForMember(dto => dto.FirstName,
                            options => options.MapFrom(barber => barber.User.FirstName))
                .ForMember(dto => dto.LastName,
                            options => options.MapFrom(barber => barber.User.LastName));

            // ADD
            CreateMap<BarberCreateDto, Barber>();

            // EDIT
            CreateMap<BarberUpdateDto, Barber>();
        }
    }
}
