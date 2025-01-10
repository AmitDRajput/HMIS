using HMIS.DataAccess.Context;
using HMIS.Domain.Entities;
using HMIS.Domain.Repository;

namespace HMIS.DataAccess.Implementation
{
    public class BloodbankMasterRepository : GenericRepository<BloodbankMaster>, IBloodbankMasterRepository
    {
        public BloodbankMasterRepository(HMISDbContext context) : base(context)
        {

        }
    }

    
}
