using HospitalSystem.Data;
using HospitalSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystem.Controllers
{
    public class AppointmentController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public IActionResult Index(string name, string specialization, int page = 1)
        {
            int pageSize = 3; // 3 doctors per page like the screenshot

            var doctors = context.Doctors.Include(d => d.Specialization).AsQueryable();

            if (!string.IsNullOrEmpty(name))
                doctors = doctors.Where(d => d.Name.Contains(name));

            if (!string.IsNullOrEmpty(specialization))
                doctors = doctors.Where(d => d.Specialization.Name.Contains(specialization));

            int totalDoctors = doctors.Count();
            int totalPages = (int)Math.Ceiling(totalDoctors / (double)pageSize);

            var pagedDoctors = doctors.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.NameFilter = name;
            ViewBag.SpecFilter = specialization;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View(pagedDoctors);
        }
        public IActionResult Book(int doctorId)
        {
            var doctor = context.Doctors.Include(d => d.Specialization).FirstOrDefault(d => d.Id == doctorId);
            if (doctor == null) return NotFound();

            ViewBag.Doctor = doctor;
            return View();
        }

        [HttpPost]
        public IActionResult Book(int doctorId, string patientName, DateTime appointmentDate, DateTime appointmentTime)
        {
            var doctor = context.Doctors.Include(d => d.Specialization).FirstOrDefault(d => d.Id == doctorId);
            ViewBag.Doctor = doctor;

            bool alreadyBooked = context.Appointments.Any(a =>
                a.DoctorId == doctorId &&
                a.AppointmentDate.Date == appointmentDate.Date &&
                a.AppointmentTime.Hour == appointmentTime.Hour &&
                a.AppointmentTime.Minute == appointmentTime.Minute
            );

            if (alreadyBooked)
            {
                ViewBag.Error = "This time slot is already booked for this doctor.";
                return View();
            }

            var appointment = new Appointment
            {
                PatientName = patientName,
                DoctorId = doctorId,
                AppointmentDate = appointmentDate,
                AppointmentTime = appointmentTime
            };

            context.Appointments.Add(appointment);
            context.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult AllAppointments()
        {
            var appointments = context.Appointments.Include(a => a.Doctor).ThenInclude(d => d.Specialization).ToList();
            return View("AllAppointments", appointments);
        }
    }
}
