using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Domain.Entities
{
    public class Departments // Add Departments
    {
        [Key]
        public int DepartmentId { get; set; }

        public string? DepartmentName { get; set; }

        public DateTime? DepartmentDate { get; set; }

        public string? DepartmentHead { get; set; } // The Name of Department Head

        public string? Description { get; set; }   // The Description of department e.g. Gynacology, Surgical etc

        public bool? IsActive { get; set; } = true;


    }
}
