using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Domain.Entities
{
    public class Holiday
    {
        public int HolidayID { get; set; }                   // Unique identifier for the holiday record
        public string HolidayName { get; set; }               // Name of the holiday
        public string Shift { get; set; }                     // Shift(s) applicable (e.g., All Shifts, Day Shifts, Night Shifts)
        public DateTime Date { get; set; }                    // Date of the holiday
        public string HolidayType { get; set; }               // Type of holiday (e.g., National, Religious, Awareness)
        public string CreatedBy { get; set; }                  // Name of the person who created the holiday entry
        public DateTime CreationDate { get; set; }             // Date and time when the holiday was created
        public string ApprovalStatus { get; set; }            // Status of approval (e.g., Approved, Rejected)
        public string Details { get; set; }                   // Additional details or description of the holiday
        public string Actions { get; set; }                   // Any additional actions required (optional)
        public bool? IsActive { get; set; }=true;


        
       

    }
}
