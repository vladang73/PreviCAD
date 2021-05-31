using Previgesst.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Previgesst.Repositories
{
    public class CadenasRepository:RepositoryBase<Cadenas>

    {
        public CadenasRepository(DbContext context) : base(context) { }
    }
}