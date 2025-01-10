using HMIS.DataAccess.Context;
using HMIS.Domain.Entities;
using HMIS.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.DataAccess.Implementation
{
    public class InsuranceProviderRepository : GenericRepository<InsuranceProvider>, IInsuranceProviderRepository
    {
        public InsuranceProviderRepository(HMISDbContext context) : base(context)
        {

        }
    }
}
