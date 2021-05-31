using Previgesst.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Previgesst.Repositories
{
    public class SectionRepository:RepositoryBase<Section>
    {
        public SectionRepository(System.Data.Entity.DbContext context) : base(context) { }


        public override Section Get(int id)
        {
            return dbSet.Include(Z => Z.ApplicationPrevi).Where(x => x.SectionId == id).FirstOrDefault();
               

        }
      

    }
}