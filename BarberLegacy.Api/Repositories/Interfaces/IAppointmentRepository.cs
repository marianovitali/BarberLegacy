using BarberLegacy.Api.Entities;

namespace BarberLegacy.Api.Repositories.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<IEnumerable<Appointment>> GetAllClientAppointmentsAsync(int clientId);
        Task<IEnumerable<Appointment>> GetAllBarberAppointmentsAsync(int barberId);
        Task<IEnumerable<Appointment>> GetAllBarberAppointmentsByDateAsync(int barberId, DateTime date);
        Task<Appointment?> GetByIdAsync(int id);
        Task<Appointment> AddAsync(Appointment appointment); 
        Task UpdateAsync(Appointment appointment);
        Task SoftDeleteAsync(Appointment appointment);
    }
}
