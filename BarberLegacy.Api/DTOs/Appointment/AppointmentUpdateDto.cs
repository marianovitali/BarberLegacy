using BarberLegacy.Api.Enums;

namespace BarberLegacy.Api.DTOs.Appointment
{
    public class AppointmentUpdateDto
    {
        public int BarberId { get; set; }
        public int ServiceId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public AppointmentStatus Status { get; set; }
    }
}
