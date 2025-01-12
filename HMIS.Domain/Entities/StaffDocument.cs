using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Domain.Entities
{
    public class StaffDocument
    {
        [Key]
        public int StaffDocumentId { get; set; }
        public int StaffId { get; set; } 
        public string? DocumentName { get; set; } 
        public string? DocumentPath { get; set; } 
        
    }

}
