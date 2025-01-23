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
    public class LeaveMasterRepository : GenericRepository<LeaveMaster>, ILeaveMasterRepository
    {
        public LeaveMasterRepository(HMISDbContext context) : base(context)
        {

        }
    }
}
