using BarberLegacy.Api.Data;
using BarberLegacy.Api.Entities;
using BarberLegacy.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BarberLegacy.Api.Repositories.Implementations
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationDbContext _context;

        public AppointmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Appointment> AddAsync(Appointment appointment)
        {
            await _context.Appointments.AddAsync(appointment);
            await _context.SaveChangesAsync();

            return appointment;
        }

        public async Task<IEnumerable<Appointment>> GetAllBarberAppointmentsAsync(int barberId)
        {
            return await _context.Appointments
                .Where(x => x.IsActive && x.BarberId == barberId)
                .Include(x => x.Service)
                .Include(x => x.Barber)
                    .ThenInclude(b => b.User)
                .Include(x => x.Client)
                    .ThenInclude(c => c.User)
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetAllBarberAppointmentsByDateAsync(int barberId, DateTime date)
        {
            return await _context.Appointments
                .Where(x => x.IsActive && x.BarberId == barberId && x.Date.Date == date.Date)
                .Include(x => x.Service)
                .Include(x => x.Barber)
                    .ThenInclude(b => b.User)
                .Include(x => x.Client)
                    .ThenInclude(c => c.User)
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetAllClientAppointmentsAsync(int clientId)
        {
            return await _context.Appointments
                .Where(x => x.IsActive && x.ClientId == clientId)
                .Include(x => x.Service)
                .Include(x => x.Barber)
                    .ThenInclude(b => b.User)
                .Include(x => x.Client)
                    .ThenInclude(c => c.User)
                .ToListAsync();
        }

        public async Task<Appointment?> GetByIdAsync(int id)
        {
            return await _context.Appointments
                .Include(x => x.Service)
                .Include(x => x.Barber)
                    .ThenInclude(b => b.User)
                .Include(x => x.Client)
                    .ThenInclude(c => c.User)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task SoftDeleteAsync(Appointment appointment)
        {
            appointment.IsActive = false;
            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Appointment appointment)
        {
            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();
        }
    }
}
