using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Domain.Entities
{
    public class NavItem
    {
        [Key]
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string? Class { get; set; }
        public string? Href { get; set; }
        public string? IconClass { get; set; }
        public string? SubIconClass { get; set; }
        public string? Text { get; set; }

        public NavItem? Parent { get; set; }
        public ICollection<NavItem>? Children { get; set; }
    }
}
