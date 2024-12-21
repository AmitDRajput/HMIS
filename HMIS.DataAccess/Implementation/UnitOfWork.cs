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

        }
        public IDoctorRepository Doctor { get; private set; } 

        public IPatientRepository Patient { get; private set; }

        public IAppointmentRepository Appointment { get; private set; }


        public IUserRepository UserMaster { get; private set; }
        public IRoleMasterRepository RoleMaster { get; private set; }
        public INavItemRepository NavItem { get; private set; }

        public void Dispose()
        {
            _context.Dispose();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}
