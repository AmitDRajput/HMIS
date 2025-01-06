using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Domain.Entities
{
    public class Admission
    {
        [Key] 
        public int AdmissionID { get; set; }

        public int PatientID { get; set; }
        public int RoomID { get; set; }
        public DateTime AdmissionDate { get; set; }
        public DateTime? DischargeDate { get; set; }

        // Navigation properties
        public Patient? Patient { get; set; }
        //public RoomType? Room { get; set; }
    }

}
