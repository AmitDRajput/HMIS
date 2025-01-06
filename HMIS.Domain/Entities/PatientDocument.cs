using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Domain.Entities
{
    
    
        public class PatientDocument
        {
            
          
            public long PatientID { get; set; }
            public bool IsActiveIsDeleted { get; set; }

        [Key]
            public long DocumentID { get; set; }            // Patients Document Id
            public string DocumentName { get; set; }       // DocumentName ex. Blood Report, X-ray, Prescription etc.
            public string DocumentPath { get; set; }       // Patients Document path

    }
    
}
