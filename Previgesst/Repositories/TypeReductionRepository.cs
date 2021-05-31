using Previgesst.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Previgesst.Repositories
{
    public class TypeReductionRepository:RepositoryBase<TypeReduction>
    {
                public TypeReductionRepository(DbContext context) : base(context) { }

        public bool estUtilise(int id)
        {
            return false;
        }
    }
}