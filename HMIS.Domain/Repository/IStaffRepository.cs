using HMIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Domain.Repository
{
    public interface IStaffRepository : IGenericRepository<Staff>
    {
        IEnumerable<StaffDto> FindDoctor(string docName);
        IEnumerable<Specilization> GetSpecilzation();

        IEnumerable<StaffDto> FindDoctorBySpecilization(string specialName);
    }
}
