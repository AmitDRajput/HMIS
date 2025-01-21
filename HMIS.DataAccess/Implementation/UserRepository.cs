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
    public class UserRepository : GenericRepository<UserMaster>, IUserRepository
    {
        public UserRepository(HMISDbContext context) : base(context)
        {

        }

        public UserMaster GetUserByUsernameAndPassword(string username, string password)
        {
            
            return _context.UserMaster.FirstOrDefault(u => u.Username == username && u.Password == password && u.IsActive==true);


        }
    }
}
