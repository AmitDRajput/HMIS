using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Domain.Entities
{
    public class Billing
    {
        [Key]
        public int BillID { get; set; }
        public int PatientID { get; set; }
        public int AppointmentID { get; set; }
        public DateTime BillDate { get; set; } = DateTime.Now;
        public decimal TotalAmount { get; set; }
        public string PaymentStatus { get; set; } = "Unpaid";

        // Navigation properties
        public Patient? Patient { get; set; }
        public Appointment? Appointment { get; set; }
    }

}
