﻿using System;
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
        public int PatientID { get; set; }       // Primary key
        public string FirstName { get; set; }    // First name of the patient
        public string LastName { get; set; }     // Last name of the patient
        public DateTime DateOfBirth { get; set; } // Date of birth
        public string Gender { get; set; }       // Gender
        public string Phone { get; set; }        // Phone number
        public string Address { get; set; }      // Address

        // Navigation properties
        public virtual ICollection<Appointment>? Appointments { get; set; } // One-to-many relationship with Appointments
        public virtual ICollection<MedicalRecord>? MedicalRecords { get; set; } // One-to-many relationship with MedicalRecords
    }


}