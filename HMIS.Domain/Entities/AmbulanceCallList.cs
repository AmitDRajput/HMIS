using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Domain.Entities
{
  public class AmbulanceCallList
    {
        [Key]
        public int AmbulanceID {  get; set; }

        public int CaseNumber { get; set; }
        public string PatientName { get; set; }
        public string PatientNumber  { get; set; } 
        public string Gender { get; set; }
        public string VehicleNumber  { get; set; }
        public string DriverName    { get; set; }
        public string DriverNumber { get; set; }

        public string PatientAddress { get; set; }

        public bool IsActive { get; set; }
    }
}
