using Previgesst.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Previgesst.Repositories
{
    public class EquipementArticuloRepository : RepositoryBase<EquipementArticulo>
    {
        public EquipementArticuloRepository(DbContext context) : base(context)
        {

        }
        public override EquipementArticulo Get(int id)
        {
            return dbSet.Include(Z => Z.Equipement)
                        .Where(x => x.EquipementArticuloID == id)
                        .FirstOrDefault();
        }
    }
}