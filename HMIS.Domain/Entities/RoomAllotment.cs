using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Domain.Entities
{
    public class RoomAllotment
    {

        public int RoomNo { get; set; }

        public string PatientName {  get; set; }

        public string Age { get; set; }
        public string RoomType { get; set; }

        public string BedNo { get; set; }

        public DateTime AddmissionDate { get; set; }

        public DateTime DischageDate { get; set; }

        public string Gender { get; set; }

        public string MobileNo { get; set; }

        public string DoctorAssigned { get; set; }

        public string Status { get; set; }

        public string AmountCharged { get; set; }

        
        public string Actions { get; set; }

        public bool IsActivated { get; set; }
        public int RoomId { get; set; }

       
    }
}
