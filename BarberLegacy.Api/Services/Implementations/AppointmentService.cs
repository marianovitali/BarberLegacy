using AutoMapper;
using BarberLegacy.Api.DTOs.Appointment;
using BarberLegacy.Api.DTOs.Barber;
using BarberLegacy.Api.Entities;
using BarberLegacy.Api.Enums;
using BarberLegacy.Api.Repositories.Interfaces;
using BarberLegacy.Api.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BarberLegacy.Api.Services.Implementations
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IBarberScheduleRepository _barberScheduleRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;

        public AppointmentService(IAppointmentRepository appointmentRepository, IBarberScheduleRepository barberScheduleRepository,
                                    IServiceRepository serviceRepository, IMapper mapper)
        {
            _appointmentRepository = appointmentRepository;
            _barberScheduleRepository = barberScheduleRepository;
            _serviceRepository = serviceRepository;
            _mapper = mapper;
        }

        public async Task<AppointmentResponseDto> CreateAsync(AppointmentCreateDto dto)
        {
            if (dto.Date.Date < DateTime.Today || (dto.Date.Date == DateTime.Today && dto.StartTime < DateTime.Now.TimeOfDay))
                return null!;

            var serviceToApply = await _serviceRepository.GetByIdAsync(dto.ServiceId);
            if (serviceToApply == null) return null!;

            if (!await ValidateClientCanBookAsync(dto.ClientId)) return null!;

            var calculatedEndTime = dto.StartTime.Add(TimeSpan.FromMinutes(serviceToApply.DurationMinutes));
            if (!await ValidateAppointmentRulesAsync(dto.BarberId, dto.Date, dto.StartTime, calculatedEndTime)) return null!;

            var appointmentEntity = _mapper.Map<Appointment>(dto);
            appointmentEntity.EndTime = calculatedEndTime;
            appointmentEntity.Status = AppointmentStatus.Confirmed;
            appointmentEntity.CreatedAt = DateTime.Now;

            await _appointmentRepository.AddAsync(appointmentEntity);
            var completeAppointment = await _appointmentRepository.GetByIdAsync(appointmentEntity.Id);

            return _mapper.Map<AppointmentResponseDto>(completeAppointment);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(id);
            if (appointment == null)
            {
                return false;
            }

            await _appointmentRepository.SoftDeleteAsync(appointment);

            return true;
        }

        public async Task<IEnumerable<AppointmentResponseDto>> GetAllBarberAsync(int barberId)
        {
            var barberAppointments = await _appointmentRepository.GetAllBarberAppointmentsAsync(barberId);

            return _mapper.Map<IEnumerable<AppointmentResponseDto>>(barberAppointments);
        }

        public async Task<IEnumerable<AppointmentResponseDto>> GetAllBarberByDateAsync(int barberId, DateTime date)
        {
            var barberAppointmentsAndDate = await _appointmentRepository.GetAllBarberAppointmentsByDateAsync(barberId, date);

            return _mapper.Map<IEnumerable<AppointmentResponseDto>>(barberAppointmentsAndDate);
        }

        public async Task<IEnumerable<AppointmentResponseDto>> GetAllClientAsync(int clientId)
        {
            var clientAppointment = await _appointmentRepository.GetAllClientAppointmentsAsync(clientId);

            return _mapper.Map<IEnumerable<AppointmentResponseDto>>(clientAppointment);
        }

        public async Task<IEnumerable<TimeSpan>> GetAvailableSlotsAsync(int barberId, DateTime date) //ACAA!!
        {
            var availableSlots = new List<TimeSpan>();

            // complete agenda
            var allSchedules = await _barberScheduleRepository.GetByBarberIdAsync(barberId);

            // just 1 day
            var schedule = allSchedules.FirstOrDefault(s => s.DayOfWeek == date.DayOfWeek);

            if (schedule == null) return availableSlots;

            // cuales ocupados?
            var bookedAppointments = await _appointmentRepository.GetAllBarberAppointmentsByDateAsync(barberId, date);

            TimeSpan slotDuration = TimeSpan.FromHours(1);
            TimeSpan currentTime = schedule.StartTime;

            while (currentTime.Add(slotDuration) <= schedule.EndTime)
            {
                bool isOccupied = bookedAppointments.Any(app => app.StartTime == currentTime);

                if (!isOccupied)
                {
                    availableSlots.Add(currentTime);
                }
                currentTime = currentTime.Add(slotDuration);
            }

            return availableSlots;
        }

        public async Task<AppointmentResponseDto?> GetByIdAsync(int id)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(id);

            if (appointment == null)
            {
                return null;
            }

            return _mapper.Map<AppointmentResponseDto>(appointment);
        }

        public async Task<AppointmentResponseDto?> UpdateAsync(AppointmentUpdateDto dto, int id)
        {
            var existingAppointment = await _appointmentRepository.GetByIdAsync(id);
            if (existingAppointment == null)
            {
                return null;
            }

            var serviceToApply = await _serviceRepository.GetByIdAsync(dto.ServiceId);
            if (serviceToApply == null)
            {
                return null;
            }

            var calculatedEndTime = dto.StartTime.Add(TimeSpan.FromMinutes(serviceToApply.DurationMinutes));

            bool isValid = await ValidateAppointmentRulesAsync(dto.BarberId, dto.Date, dto.StartTime, calculatedEndTime, id);

            if (!isValid)
            {
                return null;
            }

            _mapper.Map(dto, existingAppointment);
            existingAppointment.EndTime = calculatedEndTime;
            await _appointmentRepository.UpdateAsync(existingAppointment);

            return _mapper.Map<AppointmentResponseDto>(existingAppointment);
        }

        private async Task<bool> ValidateAppointmentRulesAsync(int barberId, DateTime date, TimeSpan startTime
                                                        , TimeSpan endTime, int? appointmentIdToIgnore = null)
        {
            var barberSchedules = await _barberScheduleRepository.GetByBarberIdAsync(barberId);

            var scheduleForDay = barberSchedules.FirstOrDefault(s => s.DayOfWeek == date.DayOfWeek);

            if (scheduleForDay == null)
            {
                return false; // barbero no trabaja
            }

            if (startTime < scheduleForDay.StartTime || endTime > scheduleForDay.EndTime)
            {
                return false; // turno fuera horario
            }

            var existingAppointments = await _appointmentRepository.GetAllBarberAppointmentsByDateAsync(barberId, date);

            bool hasOverlap = existingAppointments.Any(a =>
                a.Id != appointmentIdToIgnore && 
                startTime < a.EndTime &&
                endTime > a.StartTime);

            if (hasOverlap)
            {
                return false; 
            }

            return true;
        }

        private async Task<bool> ValidateClientCanBookAsync(int clientId)
        {
            var clientAppointments = await _appointmentRepository.GetAllClientAppointmentsAsync(clientId);

            var activeFutureAppointments = clientAppointments.Count(a =>
                a.Date >= DateTime.Today &&
                (a.Status == AppointmentStatus.Pending || a.Status == AppointmentStatus.Confirmed) &&
                a.IsActive);

            return activeFutureAppointments < 3; 
        }
    }
}
