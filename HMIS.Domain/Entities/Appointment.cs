using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Domain.Entities
{
    public class Appointment
    {
        [Key]
        public int AppointmentID { get; set; }  // Primary key
        public int PatientID { get; set; }      // Foreign key to Patients
        public int DoctorID { get; set; }       // Foreign key to Doctors
        public DateTime AppointmentDate { get; set; } // Appointment date and time
        public string? ReasonForVisit { get; set; }   // Reason for visit (nullable)
        public string Status { get; set; } = "Scheduled";  // Default status is "Scheduled"

        public bool IsActive { get; set; }
        // Navigation properties
        public virtual Patient? Patient { get; set; }  // Relationship to Patient
        public virtual Doctor? Doctor { get; set; }    // Relationship to Doctor
    }


}
