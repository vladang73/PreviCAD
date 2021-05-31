using Previgesst.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Previgesst.Repositories
{
    public class EquipementRepository : RepositoryBase<Equipement>
    {
        public EquipementRepository(DbContext context) : base(context)
        {

        }
        public override Equipement Get(int id)
        {
            return dbSet.Include(Z => Z.Client)
                        .Include(Z => Z.EquipementArticulo)
                        .Where(x => x.EquipementId == id)
                        .FirstOrDefault();
        }
    }
}