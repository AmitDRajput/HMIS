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

        int Save();
    }
}
