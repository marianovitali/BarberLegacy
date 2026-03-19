using AutoMapper;
using BarberLegacy.Api.DTOs.BarberShop;
using BarberLegacy.Api.Entities;

namespace BarberLegacy.Api.Mappings
{
    public class BarberShopProfile : Profile
    {
        public BarberShopProfile()
        {
            CreateMap<BarberShop, BarberShopResponseDto>();

            CreateMap<BarberShopCreateDto, BarberShop>();

            CreateMap<BarberShopUpdateDto, BarberShop>();
        }
    }
}
