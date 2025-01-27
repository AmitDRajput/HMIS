using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Domain.Entities
{
    public class Staff
    {
        [Key]
        public long StaffID { get; set; } // Primary Key

        [MaxLength(50)]
        public string? FirstName { get; set; } = string.Empty;

        public string? specilization { get;set; }

        [MaxLength(50)]
        public string? LastName { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? Designation { get; set; } = string.Empty;

        [MaxLength(12)]
        [Phone]
        public string? MobileNo { get; set; }

        [MaxLength(200)]
        [EmailAddress]
        public string? Email { get; set; }

        public bool? IsActive { get; set; } = true; // Default to true

        [MaxLength(400)]
        public string? StaffPic { get; set; } // Staff Profile Picture

        public DateTime? HireDate { get; set; }

        [MaxLength(10)]
        public string? Gender { get; set; } // Gender of the Employee

        public DateTime? DateOfJoining { get; set; } // Joining Date

        [MaxLength(500)]
        public string? Address { get; set; } // Residential Address

        [MaxLength(50)]
        public string? Salary { get; set; } // Salary Details

        [MaxLength(50)]
        public string? Shift { get; set; } // Work Shift Details


        [MaxLength(1000)]
        public string? Description { get; set; }
        public DateTime? DateOfBirth { get; set; }

        [MaxLength(50)]
        public string? Password { get; set; } // Recommend hashing passwords

        [MaxLength(100)]
        public string? Education { get; set; } // Education Details

        public int? DepartmentID { get; set; } // Foreign Key to Department

        public int? UserTypeId { get; set; } // Foreign Key to UserType
}
    public class UserType
    {
        [Key]
        public int UserTypeId { get; set; } // Primary Key

        [MaxLength(100)]
        public string? TypeName { get; set; } // Example: Admin, Manager, Employee

        public bool? IsActive { get; set; } // Indicates if the UserType is active
    }

    public class Specilization
    {
        [Key]
        public int SepcialId { get; set; } // Primary Key

        [MaxLength(150)]
        public string? SpecilName { get; set; } = string.Empty;
    }
    public class StaffDto
    {
        public long StaffID { get; set; } // Primary Key

        public string? FirstName { get; set; }

        public string? specilization { get; set; }
        public string? LastName { get; set; }

        public string? StaffName { get; set; }

        public string? Designation { get; set; }

        public string? MobileNo { get; set; }

        public string? Email { get; set; }

        public bool? IsActive { get; set; }

        public string? StaffPic { get; set; } // Staff Profile Picture

        public DateTime? HireDate { get; set; }

        public string? Gender { get; set; }

        public DateTime? DateOfJoining { get; set; }

        public string? Address { get; set; }

        public string? Salary { get; set; }

        public string? Shift { get; set; }

        public string? Description { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? Education { get; set; }

        public int? DepartmentID { get; set; }

        public int? UserTypeId { get; set; }

        public string? UserTypeName { get; set; } // Added for convenience to show UserType details
    }

}


