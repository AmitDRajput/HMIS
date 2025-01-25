using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Domain.Entities
{
    public class Patient
    {
        [Key]
        public long PatientID { get; set; }       // Primary key
       
        public string FirstName { get; set; }    // First name of the patient
        public string LastName { get; set; }     // Last name of the patient
        public DateTime DateOfBirth { get; set; } // Date of birth

        public DateTime DateOfAddmition { get; set; }
        public string Diagnosis {  get; set; }

        public string LabReports { get; set; }
        public string Gender { get; set; }       // Gender
        public string? MobileNumber { get; set; }        // MobileNumber number
        public string? Address { get; set; }      // Address
        public string? BloodGroup { get; set; }   // Blood Group
        public string? BloodPressure { get; set; } // Present Blood Pressure
        public string? Sugar { get; set; }       // Present Suger
        public string? Injury { get; set; }      // Address
        public bool IsActive{ get; set; }        // IS Active Flag For Patient
        public string? PatientPic { get; set; }      // Patient Reports and History Documents

        public String NextFollowUp { get; set; }

        public String Staus { get; set; }

        public String Action { get;set; }

        // Navigation properties
        //public virtual ICollection<Appointment>? Appointments { get; set; } // One-to-many relationship with Appointments
        //public virtual ICollection<MedicalRecord>? MedicalRecords { get; set; } // One-to-many relationship with MedicalRecords
    }


}
