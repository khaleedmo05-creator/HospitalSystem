using HospitalSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog = Hospital; Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Doctor>().HasData(
                new Doctor { Id = 1, Name = "Dr. Smith Michael", SpecializationId = 1, Image = "doctor1.jpg" },
                new Doctor { Id = 2, Name = "Dr. Bob Charlie", SpecializationId = 2, Image = "doctor2.jpg" },
                new Doctor { Id = 3, Name = "Dr. Brown Alice", SpecializationId = 3, Image = "doctor3.jpg" },
                new Doctor { Id = 4, Name = "Dr. Brown", SpecializationId = 4, Image = "doctor4.jpg" },
                new Doctor { Id = 5, Name = "Dr. Alice", SpecializationId = 5, Image = "doctor5.jpg" },
                new Doctor { Id = 6, Name = "Dr. pedro", SpecializationId = 6, Image = "doctor6.jpg" }
            );
            modelBuilder.Entity<Specialization>().HasData(
                new Specialization { Id = 1, Name = "Pediatrics" },
                new Specialization { Id = 2, Name = "Neurology" },
                new Specialization { Id = 3, Name = "Dermatology" },
                new Specialization { Id = 4, Name = "Pediatrics" },
                new Specialization { Id = 5, Name = "Neurology" },
                new Specialization { Id = 6, Name = "Dermatology" }
            );
            modelBuilder.Entity<Appointment>().HasData(
                new Appointment { Id = 1, DoctorId = 1, PatientName = "Brown ", AppointmentDate = new DateTime(2026, 4, 17, 17, 41, 57) },
                new Appointment { Id = 2, DoctorId = 2, PatientName = "Smith", AppointmentDate = new DateTime(2026, 4, 18, 17, 41, 57) },
                new Appointment { Id = 3, DoctorId = 3, PatientName = "Joao", AppointmentDate = new DateTime(2026, 4, 19, 17, 41, 57) },
                new Appointment { Id = 4, DoctorId = 4, PatientName = "Brown ", AppointmentDate = new DateTime(2026, 4, 20, 17, 41, 57) },
                new Appointment { Id = 5, DoctorId = 5, PatientName = "Smith", AppointmentDate = new DateTime(2026, 4, 21, 17, 41, 57) },
                new Appointment { Id = 6, DoctorId = 6, PatientName = "Joao", AppointmentDate = new DateTime(2026, 4, 22, 17, 41, 57) }
            );
            
        }
    }
}