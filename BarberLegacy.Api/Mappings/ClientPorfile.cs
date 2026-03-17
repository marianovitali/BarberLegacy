using AutoMapper;
using BarberLegacy.Api.DTOs.Client;
using BarberLegacy.Api.Entities;

namespace BarberLegacy.Api.Mappings
{
    public class ClientPorfile : Profile
    {
        public ClientPorfile()
        {
            CreateMap<Client, ClientResponseDto>()
                .ForMember(dto => dto.FirstName, opt => opt.MapFrom(client => client.User.FirstName))
                .ForMember(dto => dto.LastName, opt => opt.MapFrom(client => client.User.LastName))
                .ForMember(dto => dto.Email, opt => opt.MapFrom(client => client.User.Email))
                .ForMember(dto => dto.PhoneNumber, opt => opt.MapFrom(client => client.User.PhoneNumber));

            CreateMap<ClientCreateDto, Client>();

            CreateMap<ClientUpdateDto, Client>();
        }
    }
}
