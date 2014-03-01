﻿using Gaz.Models.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;

namespace Gaz.DAL.Repositories
{
    public class AddressesRepository : GenericRepository<Address>
    {
        public AddressesRepository(DbContext context) : base(context) { }

    }
}
