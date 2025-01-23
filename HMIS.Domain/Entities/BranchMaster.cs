using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Domain.Entities
{
    public class BranchMaster
    {

        [Key]
            public long BranchId { get; set; } // Primary Key
            public string? BranchName { get; set; }
            public string? BranchHead { get; set; }
            public string? BranchContactDetails { get; set; }
            public string? BranchAddress { get; set; }
            public DateTime? CreatedOn { get; set; }
            public bool? IsActive { get; set; }
        

    }
}
