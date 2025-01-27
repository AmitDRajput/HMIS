using HMIS.DataAccess.Context;
using HMIS.Domain.Entities;
using HMIS.Domain.Repository;
using Microsoft.EntityFrameworkCore;
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

        public IEnumerable<LeaveMaster> GetAllLeaves()
        {
            return (from leave in _context.leaveMaster
                    join staff in _context.Staff
                    on leave.StaffID equals staff.StaffID
                    where leave.IsActive == true
                    select new LeaveMaster
                    {
                        LeaveID = leave.LeaveID,
                        LeaveDateFrom = leave.LeaveDateFrom,
                        LeaveDateTo = leave.LeaveDateTo,
                        StaffID = leave.StaffID,
                        ReasonOfLeave = leave.ReasonOfLeave,
                        TypeOfLeave = leave.TypeOfLeave,
                        IsActive = leave.IsActive,
                        StaffName = staff.FirstName + " " + staff.LastName // Populate StaffName here
                    })
                    .OrderByDescending(leave => leave.LeaveID)
                    .ToList();
        }


        public IEnumerable<LeaveMaster> GetByStaffIDAndStartDate(long staffId, DateTime startTime, DateTime endTime)
        {
            return _context.leaveMaster
                .Where(l => l.StaffID == staffId && l.LeaveDateFrom >= startTime && l.LeaveDateTo >= endTime)
                .ToList();
        }

        public LeaveMaster GetByStaffIDAndStartDate(long staffId, DateTime startTime)
        {
            return _context.leaveMaster
        .Where(l => l.StaffID == staffId &&
                    l.LeaveDateFrom.HasValue &&
                    l.LeaveDateFrom.Value.Date == startTime.Date)
        .FirstOrDefault();
        }

        public bool IsLeaveOnHoliday(long staffId, DateTime startTime)
        {
            // Check if the staff has a leave record on the specified date
            var leaveRecord = _context.leaveMaster
                .Where(l => l.StaffID == staffId &&
                            l.LeaveDateFrom.HasValue &&
                            l.LeaveDateFrom.Value.Date == startTime.Date)
                .FirstOrDefault();

            if (leaveRecord == null)
            {
                return false; // No leave record found for the given staff on the specified date
            }

            // Check if the date is a holiday in HolidayMaster
            bool isHoliday = _context.Holiday
                .Any(h => h.Date.Date == startTime.Date); // Assumes HolidayDate is DateTime

            // Return true if there is a leave record and it's a holiday
            if (isHoliday == false && leaveRecord != null)
                return true;

            return isHoliday;
        }

      
    }
}
