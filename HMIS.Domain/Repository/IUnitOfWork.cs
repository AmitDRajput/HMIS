using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Domain.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IDoctorRepository Doctor { get; }

        IPatientRepository Patient { get; }
        IAppointmentRepository Appointment { get; }

        IUserRepository UserMaster { get; }

        IUserTypeRepository UserType { get; }

        IRoleMasterRepository RoleMaster { get; }

        INavItemRepository NavItem { get; }

        IPatientDocumentRepository PatientDocument { get; }
        IStaffDocumentRepository StaffDocument { get; }
        IStaffRepository Staff { get; }

        IRoomTypesRepository RoomTypes { get; }

        IDepartmentsRepository Departments { get; }

        IBloodbankMasterRepository BloodbankMaster { get; }

        IBloodStorageRepository BloodStorage { get; }

        IBloodDonorRepository BloodDonor { get; }

        IInsuranceProviderRepository InsuranceProvider { get; }

        IHolidayRepository Holiday { get; }

        IAmbulanceCallListRepository AmbulanceCallList { get; }

        IBranchMasterRepository BranchMaster { get; }

        ILeaveMasterRepository LeaveMaster { get; }

        //IRoomAllotmentRepository RoomAllotment { get; }
        int Save();

        void Detach<TEntity>(TEntity entity) where TEntity : class;
    }
}
