using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Domain.Entities
{
    public class Doctor
    {
        [Key]
        public int DoctorID { get; set; }        // Primary key
        public string DoctorName { get; set; }   // Name of the doctor
        public string Specialty { get; set; }    // Specialty field
        public string Phone { get; set; }        // Contact number
        public int DepartmentID { get; set; }    // Foreign key to Department
        public bool IsActive { get; set; }       // Flag For Doctor 
        // Navigation properties
        //public virtual Department? Department { get; set; }  // Relationship to Department
        //public virtual ICollection<Appointment>? Appointments { get; set; } // One-to-many relationship with Appointments
        //public virtual ICollection<MedicalRecord>? MedicalRecords { get; set; } // One-to-many relationship with MedicalRecords
    }


}
