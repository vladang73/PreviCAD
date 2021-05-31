using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Previgesst.Helpers
{
    public static class ControllerExtensions
    {
        /// <summary>
        /// Return the name of the controller wihout Controller suffix.
        /// </summary>
        /// <param name="controller">Controller to extract the name</param>
        /// <returns>The URL name of the controller</returns>
        public static string GetUrlName(this Type controller)
        {
            var name = controller.Name;
            if(name.EndsWith("Controller", StringComparison.InvariantCultureIgnoreCase))
            {
                return name.Substring(0, name.Length - 10);
            }
            else
            {
                throw new ArgumentException("Le Type doit être un System.Web.Mvc.Controller.", nameof(controller));
            }
        }
    }
}