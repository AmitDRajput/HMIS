using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Domain.Entities
{
    public class Staff
    {
        [Key]
        public int StaffID { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Designation { get; set; } = string.Empty;
        public string? MobileNo { get; set; }
        public string? Email { get; set; }

        public bool IsActive { get; set; }
        public string? StaffPic { get; set; }      // Staff ProfileDocuments
        public DateTime HireDate { get; set; }
        public string Gender { get; set; } // Gender of the employee
       
        public string DateOfJoining { get; set; } // Joining date (recommend using DateTime)
        public string Address { get; set; } // Residential address
        public string Salary { get; set; } // Salary details
        public string Shift { get; set; } // Work shift details

        public string Actions { get; set; } // Actions (customizable admin-defined tasks)
        public string DateOfBirth   { get; set; }

        public string Password { get; set; }

        public string ReEnterPassword { get; set; }


        public string Education { get; set; }



    }

}
