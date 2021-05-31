using Previgesst.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Previgesst.Repositories
{
    public class ApplicationPreviRepository:RepositoryBase<ApplicationPrevi>
    {
        public ApplicationPreviRepository(DbContext context) : base(context) { }

      
    }
}