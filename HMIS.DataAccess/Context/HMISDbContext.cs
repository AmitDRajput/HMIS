using HMIS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.DataAccess.Context
{
    public class HMISDbContext : DbContext
    {
        public HMISDbContext(DbContextOptions<HMISDbContext> options) : base(options)
        {
            
        }
        public DbSet<Patient> Patients { get; set; }

        public DbSet<PatientDocument> PatientDocument { get; set; }

        public DbSet<StaffDocument> StaffDocument { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<Billing> Billings { get; set; }
        public DbSet<Staff> Staff { get; set; }
        //public DbSet<RoomType> Rooms { get; set; }
        public DbSet<Admission> Admissions { get; set; }

        public DbSet<UserMaster> UserMaster { get; set; }

        public DbSet<RoomTypes> roomTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure relationships, keys, etc.
            //modelBuilder.Entity<Appointment>()
            //    .HasOne(a => a.Patient)
            //    .WithMany(p => p.Appointments)
            //    .HasForeignKey(a => a.PatientID);

            //modelBuilder.Entity<Appointment>()
            //    .HasOne(a => a.Doctor)
            //    .WithMany(d => d.Appointments)
            //    .HasForeignKey(a => a.DoctorID);

            //modelBuilder.Entity<MedicalRecord>()
            //    .HasOne(m => m.Patient)
            //    .WithMany(p => p.MedicalRecords)
            //    .HasForeignKey(m => m.PatientID);

            //modelBuilder.Entity<MedicalRecord>()
            //    .HasOne(m => m.Doctor)
            //    .WithMany(d => d.MedicalRecords)
            //    .HasForeignKey(m => m.DoctorID);

            //modelBuilder.Entity<Billing>()
            //    .HasOne(b => b.Patient)
            //    .WithMany(p => p.Billings)
            //    .HasForeignKey(b => b.PatientID);

            //modelBuilder.Entity<Billing>()
            //    .HasOne(b => b.Appointment)
            //    .WithMany()
            //    .HasForeignKey(b => b.AppointmentID);

            //modelBuilder.Entity<Admission>()
            //    .HasOne(a => a.Patient)
            //    .WithMany(p => p.Admissions)
            //    .HasForeignKey(a => a.PatientID);

            //modelBuilder.Entity<Admission>()
            //    .HasOne(a => a.Room)
            //    .WithMany(r => r.Admissions)
            //    .HasForeignKey(a => a.RoomID);
        }
    }



}
