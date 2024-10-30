using HMIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Domain.Repository
{
    public interface IUserRepository : IGenericRepository<UserMaster>
    {
     UserMaster GetUserByUsernameAndPassword(string username, string password);
    }
}
