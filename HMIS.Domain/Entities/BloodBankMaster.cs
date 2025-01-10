using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Domain.Entities
{
    public class BloodbankMaster
    {
        [Key]

        public int BloodbankMasterID { get; set; }
        public int BloodBankID { get; set; } // Primary key
        public string BloodType { get; set; } // Blood type (e.g., A+, O-, B+)
        public int Quantity { get; set; } // Quantity of blood in units
        public DateTime DonationDate { get; set; } // Date and time when the blood was donated
        public DateTime ExpiryDate { get; set; } // Expiry date for the blood
        public int? DonorID { get; set; } // Foreign key to Donor (nullable)
        public string BloodGroupCategory { get; set; } // Optional: blood group category (e.g., plasma, red blood cells, etc.)
        public string StorageCondition { get; set; } // Storage condition (e.g., refrigerated, frozen)
        public string DonorName { get; set; } // Name of the donor (optional if DonorID is used)
        public string DonorContact { get; set; } // Contact number or other details for the donor (optional)
        public string DonorEmail { get; set; } // Email of the donor (optional)
        public bool IsAvailableForUse { get; set; } // Indicates if blood is available for use
        public DateTime CreatedAt { get; set; } // Record creation timestamp
        public DateTime UpdatedAt { get; set; } // Record update timestamp

        public bool IsActive { get; set; }
        // Optionally, you can define navigation properties for the foreign key if you have a related Donor class
        // public virtual Donor Donor { get; set; }


    }
}
