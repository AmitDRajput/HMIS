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
    public class RoomTypesRepository : GenericRepository<RoomTypes>, IRoomTypesRepository
    {

        public RoomTypesRepository(HMISDbContext context) : base(context)
        {

        }
    }
}
