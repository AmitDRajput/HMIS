using HMIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Domain.Repository
{
    public interface ILeaveMasterRepository : IGenericRepository<LeaveMaster>
    {
        IEnumerable<LeaveMaster> GetByStaffIDAndStartDate(long staffId, DateTime startTime, DateTime endTime);
        LeaveMaster GetByStaffIDAndStartDate(long staffId, DateTime startTime);

        bool IsLeaveOnHoliday(long staffId, DateTime startTime);
    }
}
