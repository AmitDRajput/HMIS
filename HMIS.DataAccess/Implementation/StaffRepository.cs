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
    public class StaffRepository : GenericRepository<Staff>, IStaffRepository
    {
        public StaffRepository(HMISDbContext context) : base(context)
        {

        }


        public IEnumerable<StaffDto> FindDoctor(string docName)
        {
            return (from st in _context.Staff
                                 join ut in _context.UserType
                                 on st.UserTypeId equals ut.UserTypeId
                                 where st.IsActive == true && ut.TypeName.Contains("Doctor") && (st.FirstName.Contains(docName) || st.LastName.Contains(docName))  
                                 select new StaffDto
                                 {
                                     StaffID = st.StaffID,
                                     StaffName = st.FirstName+" "+st.LastName,
                                     UserTypeName = ut.TypeName
                                 }).ToList();
        }
        public IEnumerable<StaffDto> FindDoctorBySpecilization(string specialName)
        {
            return (from st in _context.Staff
                    join ut in _context.UserType
                    on st.UserTypeId equals ut.UserTypeId
                    where st.IsActive == true && ut.TypeName.Contains("Doctor") && st.specilization.Contains(specialName)
                    select new StaffDto
                    {
                        StaffID = st.StaffID,
                        StaffName = st.FirstName + " " + st.LastName,
                        UserTypeName = ut.TypeName
                    }).ToList();
        }

        public IEnumerable<Specilization> GetSpecilzation()
        {
            return (_context.Specilization.ToList());
                    
        }
    }
}
