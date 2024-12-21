using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HMIS.Domain.Entities
{
    public class RoleMaster
    {
        [Key]
        public int RoleMasterId { get; set; }
        public string RoleName { get; set; } = string.Empty;

        // Navigation property for the related UserMasters
        [JsonIgnore]
        public ICollection<UserMaster> UserMasters { get; set; } = new List<UserMaster>();
    }
}
