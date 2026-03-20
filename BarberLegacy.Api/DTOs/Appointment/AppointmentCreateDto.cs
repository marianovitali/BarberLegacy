namespace BarberLegacy.Api.DTOs.Appointment
{
    public class AppointmentCreateDto
    {
        public required int ClientId { get; set; }
        public required int BarberId { get; set; }
        public required int ServiceId { get; set; }
        public required DateTime Date { get; set; }
        public required TimeSpan StartTime { get; set; }
    }
}
