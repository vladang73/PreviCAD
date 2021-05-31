using Previgesst.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Previgesst.Repositories
{
    public class ProbabiliteRepository:RepositoryBase<Probabilite>
    {
        public ProbabiliteRepository(DbContext context) : base(context) { }

    }
}