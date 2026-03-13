using BarberLegacy.Api.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BarberLegacy.Api.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Barber> Barbers { get; set; }
        public DbSet<BarberShop> BarberShops { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<BarberSchedule> BarberSchedules { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Client configuration
            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(c => c.CreatedAt)
                    .HasDefaultValueSql("GETDATE()");
            });

            // Barber configuration
            modelBuilder.Entity<Barber>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(b => b.BarberShop)
                    .WithMany(bs => bs.Barbers)
                    .HasForeignKey(b => b.BarberShopId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(b => b.Appointments)
                    .WithOne(a => a.Barber)
                    .HasForeignKey(a => a.BarberId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(b => b.Schedules)
                    .WithOne(s => s.Barber)
                    .HasForeignKey(s => s.BarberId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // BarberShop configuration
            modelBuilder.Entity<BarberShop>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Name);
            });

            // Service configuration
            modelBuilder.Entity<Service>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(s => s.Price)
                    .HasPrecision(10, 2);

                entity.HasMany(s => s.Appointments)
                    .WithOne(a => a.Service)
                    .HasForeignKey(a => a.ServiceId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Appointment configuration
            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => new { e.BarberId, e.Date, e.StartTime });

                entity.HasOne(a => a.Client)
                    .WithMany(c => c.Appointments)
                    .HasForeignKey(a => a.ClientId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(a => a.CreatedAt)
                    .HasDefaultValueSql("GETDATE()");
            });

            // BarberSchedule configuration
            modelBuilder.Entity<BarberSchedule>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => new { e.BarberId, e.DayOfWeek }).IsUnique();
            });
        }
    }
}
