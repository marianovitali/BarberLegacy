using AutoMapper;
using BarberLegacy.Api.DTOs.Appointment;
using BarberLegacy.Api.DTOs.Barber;
using BarberLegacy.Api.Entities;

namespace BarberLegacy.Api.Mappings
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            //GET
            CreateMap<Appointment, AppointmentResponseDto>()
                .ForMember(dto => dto.ClientName,
                            options => options.MapFrom(client => $"{client.Client.User.FirstName} {client.Client.User.LastName}"))
                .ForMember(dto => dto.BarberName,
                            options => options.MapFrom(barber => $"{barber.Barber.User.FirstName} {barber.Barber.User.LastName}"))
                .ForMember(dto => dto.ServiceName,
                            options => options.MapFrom(service => service.Service.Name));
            // ADD
            CreateMap<AppointmentCreateDto, Appointment>();

            // EDIT
            CreateMap<AppointmentUpdateDto, Appointment>();

        }
    }
}
