using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Domain.Entities
{
    public class Room
    {
        [Key]
        public int RoomID { get; set; }
        public string RoomNumber { get; set; } = string.Empty;
        public string RoomType { get; set; } = "General";
        public string AvailabilityStatus { get; set; } = "Available";

        // Navigation properties
        public ICollection<Admission>? Admissions { get; set; }
    }

}
