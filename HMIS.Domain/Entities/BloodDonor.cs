using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Domain.Entities
{
    
    
        public class BloodDonor
        {
        [Key]
            public int BloodDonorID { get; set; }          // Unique identifier for the donor (e.g., D001)
            public string DonorName { get; set; }        // Name of the donor
            public DateTime DateOfBirth { get; set; }    // Date of birth of the donor
            public string Gender { get; set; }           // Gender of the donor (Male/Female)
            public string BloodType { get; set; }        // Blood type of the donor (e.g., O+, A-)
            public string PhoneNumber { get; set; }      // Phone number of the donor
            public string Email { get; set; }            // Email address of the donor
            public string DonorStatus { get; set; }      // Status of the donor (Active/Inactive)
            public DateTime? LastDonationDate { get; set; } // Last date of donation
            public DateTime? NextEligibleDonationDate { get; set; } // Next eligible donation date
            public string DonorLocation { get; set; }    // Blood bank location where the donor is registered
            public DateTime LastUpdated { get; set; }  

            public string DonationHistory { get; set; }  // Donor History -- Here we can undestand that Donor Blood Donation History in Counts with dates.

        
            public string DonorNotes {  get; set; }
        
            // Timestamp of the last record update
            public bool IsActive { get; set; } = true;
        }

    
}
