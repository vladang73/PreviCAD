using Previgesst.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Previgesst.Repositories
{
    public class FrequenceRepository:RepositoryBase<Frequence>
    {
        public FrequenceRepository(DbContext context) : base(context) { }
    }
}