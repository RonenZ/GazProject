using Gaz.Models.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;

namespace Gaz.DAL.Repositories
{
    public class UserRolesRepository : GenericRepository<UserRole>
    {
        public UserRolesRepository(DbContext context) : base(context) { }

    }
}
