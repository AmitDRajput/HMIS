using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Domain.Entities
{
    public class LeaveMaster
    {
        [Key]

        public long LeaveID { get; set; } // Primary Key
        public long StaffID { get; set; }  
        public DateTime? CreatedDate { get; set; }
        public DateTime? LeaveDateFrom { get; set; }
        public DateTime? LeaveDateTo { get; set; }
        public string? TypeOfLeave { get; set; }
        public string? ReasonOfLeave { get; set; }
        public string? leaveRecords { get; set; }
        public bool? IsActive { get; set; }

    }
}
