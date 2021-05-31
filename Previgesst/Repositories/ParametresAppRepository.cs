using Previgesst.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Previgesst.Repositories
{
    public class ParametresAppRepository : RepositoryBase<ParametresApp>
    {
        public ParametresAppRepository(DbContext context) : base(context)
        {
        }

        public override ParametresApp Get(int ParametresAppId)
        {
            return dbSet.Where(m => m.ParametresAppId == ParametresAppId).FirstOrDefault();
        }
    }
}