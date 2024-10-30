using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Domain.Entities
{
    public class Department
    {
        [Key]
        public int DepartmentID { get; set; }        // Primary key
        public string DepartmentName { get; set; }   // Name of the department

        // Navigation properties
        public virtual ICollection<Doctor>? Doctors { get; set; }  // One-to-many relationship with Doctors
    }


}
