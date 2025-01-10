using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Domain.Entities
{
    public class InsuranceProvider
    {
        [Key]
        public int InsuranceProviderID { get; set; }
        public string ProviderName { get; set; }
        public string ProviderCode { get; set; }  
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public string Address { get; set; }
        public string WebsiteURL { get; set; }
        public string CustomerSupportNumber { get; set; }
        public DateTime ContractStartDate { get; set; }
        public decimal ReimbursementRate { get; set; }
        public string CoverageTypes { get; set; }
        public string Status { get; set; }

        public bool IsActive { get; set; }
    }
}
