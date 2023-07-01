using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace HospitalBB.Models
{
    public class HospitalBBContextClass : DbContext
    {
        public DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>()
                .HasIndex(a => a.AdminEmail)
                .IsUnique();
        }
        public DbSet<Appointment> Appointments { get; set; }
        
        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Patient> Patients { get; set; }

        public HospitalBBContextClass(DbContextOptions<HospitalBBContextClass> options) : base(options) { }

    }


}
