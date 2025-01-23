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
        public bool IsActive { get; set; }

        public ICollection<MenuRole> MenuRoles { get; set; }
        // Navigation property for the related UserMasters
        [JsonIgnore]
        public ICollection<UserMaster> UserMasters { get; set; } = new List<UserMaster>();
    }

    // Models/Menu.cs
    public class Menu
    {
        [Key]
        public int Id { get; set; }
        public string Path { get; set; }
        public string Title { get; set; }
        public string IconType { get; set; }
        public string Icon { get; set; }
        public string Class { get; set; }
        public bool GroupTitle { get; set; }
        public string Badge { get; set; }
        public string BadgeClass { get; set; }
        public string Role { get; set; }
        public int? ParentId { get; set; }

        public Menu? Parent { get; set; }  // Navigation property
                                           // Initialize collections to avoid null references
        public ICollection<Menu> SubMenus { get; set; } = new List<Menu>();
        public ICollection<MenuRole> MenuRoles { get; set; } = new List<MenuRole>();
    }

    // Models/MenuRole.cs
    public class MenuRole
    {
        [Key]
        public int MenuId { get; set; }
        public Menu Menu { get; set; }

        public int RoleMasterId { get; set; }
        public RoleMaster RoleMaster { get; set; }
    }

    // Models/RoleMaster.cs (Assuming you have roles table for reference)

}
