using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Previgesst.ActionFilters
{
    //https://github.com/benfoster/Fabrik.Common/tree/master/src/Fabrik.Common.Web/Filters
    /// <summary>
    /// A base class for Action Filters that need to transfer ModelState to/from TempData
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    [DebuggerStepThrough]
    public abstract class ModelStateTempDataTransfer : ActionFilterAttribute
    {
        protected static readonly string Key = typeof(ModelStateTempDataTransfer).FullName;

        /// <summary>
        /// Exports the current ModelState to TempData (available on the next request).
        /// </summary>       
        protected static void ExportModelStateToTempData(ControllerContext context)
        {
            context.Controller.TempData[Key] = context.Controller.ViewData.ModelState;
        }

        /// <summary>
        /// Populates the current ModelState with the values in TempData
        /// </summary>
        protected static void ImportModelStateFromTempData(ControllerContext context)
        {
            var prevModelState = context.Controller.TempData[Key] as ModelStateDictionary;
            context.Controller.ViewData.ModelState.Merge(prevModelState);
        }

        /// <summary>
        /// Removes ModelState from TempData
        /// </summary>
        protected static void RemoveModelStateFromTempData(ControllerContext context)
        {
            context.Controller.TempData[Key] = null;
        }
    }
}