using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Domain.Entities
{
    public class MedicalRecord
    {
        [Key]
        public int RecordID { get; set; }            // Primary key
        public int PatientID { get; set; }           // Foreign key to Patient
        public int DoctorID { get; set; }            // Foreign key to Doctor
        public string Diagnosis { get; set; } = string.Empty; // Diagnosis
        public string? TreatmentDetails { get; set; }         // Treatment details (nullable)
        public string? Prescription { get; set; }             // Prescription details (nullable)
        public DateTime RecordDate { get; set; } = DateTime.Now; // Default current date

        // Navigation properties
        public virtual Patient? Patient { get; set; }         // Relationship to Patient
        public virtual Doctor? Doctor { get; set; }           // Relationship to Doctor
    }


}
