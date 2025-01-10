using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Domain.Entities
{
    public class BloodStorage
    {
        public int BloodStorageId { get; set; }
        [Key]
        public string BloodProductID { get; set; }  // Unique identifier for the blood product (e.g., BP001)
        public string BloodType { get; set; }       // Blood type (e.g., O+, A-, etc.)
        public string ComponentType { get; set; }   // Type of blood component (e.g., Red Blood Cells, Plasma)
        public int QuantityInStock { get; set; }    // Quantity in stock (e.g., 25 units)
        public DateTime ExpiryDate { get; set; }    // Expiry date for the blood product
        public DateTime CollectionDate { get; set; } // Date the blood was collected
        public string StorageLocation { get; set; }  // Location where blood is stored (e.g., Blood Bank Freezer)
        public string DonationStatus { get; set; }   // Donation status (e.g., Fresh, In-Use, Expired)
        public string BatchNumber { get; set; }      // Batch number for the blood product
        public string ConditionQualityStatus { get; set; } // Condition or quality status (e.g., Good, Hemolyzed, Clotted)
        public string TemperatureRange { get; set; } // Temperature range for storage (e.g., 2-6°C, 4°C)
        public DateTime DateLastUpdated { get; set; } // Date the record was last updated

        public bool IsActive { get; set; }

        // Additional properties for "Actions" can be added if necessary (for example, an Action method or property)
    }


}

