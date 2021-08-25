using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Previgesst.Repositories;
using Previgesst.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Previgesst.Services
{
    public class LogEntryService
    {
        private LogEntryRepository logRepo;

        public LogEntryService(LogEntryRepository logRepository)
        {
            this.logRepo = logRepository;
        }


        internal DataSourceResult GetReadListeLogs(DataSourceRequest request)
        {
            var baseURL = Helpers.URLHelper.GetBaseUrl();
            var time = DateTime.Now.ToLongTimeString();

            // prepare query
            return logRepo.AsQueryable()
                          .OrderByDescending(x => x.LogEntryId)
                          .ToDataSourceResult(request)
                          ;

        }

    }
}