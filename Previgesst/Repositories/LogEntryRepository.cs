using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Previgesst.Models;

namespace Previgesst.Repositories
{
    public class LogEntryRepository : RepositoryBase<LogEntry>
    {
        public LogEntryRepository(DbContext context) : base(context) { }
    }
}