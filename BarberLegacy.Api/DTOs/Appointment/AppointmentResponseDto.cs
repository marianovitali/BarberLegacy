using BarberLegacy.Api.Enums;

namespace BarberLegacy.Api.DTOs.Appointment
{
    public class AppointmentResponseDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int BarberId { get; set; }
        public int ServiceId { get; set; }
        public string ClientName { get; set; } = string.Empty; 
        public string BarberName { get; set; } = string.Empty; 
        public string ServiceName { get; set; } = string.Empty; 

        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; } 
        public AppointmentStatus Status { get; set; }
    }
}
