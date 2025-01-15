using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Domain.Entities
{
   
    public class RoomTypes
    {
        [Key]
        public int RoomID { get; set; }
        public string RoomNumber { get; set; } = string.Empty;
        public int RoomTypesId { get; set; } 
        public string AvailabilityStatus { get; set; } = "Available";

        public bool IsActive { get; set; }

        // Navigation properties
        public ICollection<Admission>? Admissions { get; set; }
    }

}
