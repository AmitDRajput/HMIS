﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Domain.Entities
{
    public class UserMaster
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; }
        public int? RoleMasterId { get; set; }
        public long? StaffId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Role { get; set; } = string.Empty;

        public bool IsActive { get; set; }


    }
    public class Permissions
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? FormName { get; set; }
    }


    public class UserMasterDTO
    {
        [Key]
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? EmailConfirmed { get; set; } = string.Empty;
        public string? Role { get; set; } = string.Empty;

        public string? token { get; set; } = string.Empty;

    }
}
