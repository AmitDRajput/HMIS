using HMIS.DataAccess.Context;
using HMIS.Domain.Entities;
using HMIS.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace HMIS.DataAccess.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HMISDbContext _context;
        public UnitOfWork(HMISDbContext context)
        {
            _context = context;
            Doctor = new DoctorRepository(_context);
            Appointment = new AppointmentRepository(_context);
            UserMaster = new UserRepository(_context);
            Patient = new PatientRepository(_context);
            RoleMaster = new RoleMasterRepository(_context);
            NavItem = new NavItemRepository(_context);
            PatientDocument = new PatientDocumentRepository(_context);
            Staff = new StaffRepository(_context);
            StaffDocument = new StaffDocumentRepository(_context);
            RoomTypes = new RoomTypesRepository(_context);
            Departments = new DepartmentsRepository(_context);
            BloodbankMaster = new BloodbankMasterRepository(_context);
            BloodDonor = new BloodDonorRepository(_context);
            BloodStorage = new BloodStorageRepository(_context);
            InsuranceProvider = new InsuranceProviderRepository(_context);
            Holiday = new HolidayRepository(_context);
            AmbulanceCallList = new AmbulanceCallListRepository(_context);
        }
        public IDoctorRepository Doctor { get; private set; }

        public IPatientRepository Patient { get; private set; }

        public IAppointmentRepository Appointment { get; private set; }


        public IUserRepository UserMaster { get; private set; }
        public IRoleMasterRepository RoleMaster { get; private set; }
        public INavItemRepository NavItem { get; private set; }

        public IPatientDocumentRepository PatientDocument { get; private set; }

        public IStaffDocumentRepository StaffDocument { get; private set; }
        public IStaffRepository Staff { get; private set; }

        public IRoomTypesRepository RoomTypes { get; private set; }

        public IDepartmentsRepository Departments { get; private set; }

        public IBloodbankMasterRepository BloodbankMaster { get; private set; }

        public IBloodStorageRepository BloodStorage { get; private set; }

        public IBloodDonorRepository BloodDonor { get; private set; }

        public IInsuranceProviderRepository InsuranceProvider { get; private set; }

        public IHolidayRepository Holiday { get; private set; }

        public IAmbulanceCallListRepository AmbulanceCallList { get; private set; }




        //public IRoomAllotmentRepository RoomAllotment { get; private set; }
        public void Dispose()
        {
            _context.Dispose();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
        //public int Save()
        //{
        //    try
        //    {
        //        return _context.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException ex)
        //    {
        //        foreach (var entry in ex.Entries)
        //        {
        //            if (entry.State == EntityState.Modified)
        //            {
        //                var databaseValues = entry.GetDatabaseValues();
        //                if (databaseValues != null)
        //                {
        //                    entry.OriginalValues.SetValues(databaseValues);
        //                }
        //                else
        //                {
        //                    entry.State = EntityState.Detached;
        //                }
        //            }
        //        }
        //        return _context.SaveChanges();
        //    }
        //}

    }
}
