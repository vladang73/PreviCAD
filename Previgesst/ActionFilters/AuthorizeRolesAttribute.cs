using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Previgesst.ActionFilters
{
        /// <summary>
        /// Authorize with roles. Extends <see cref="AuthorizeAttribute"/> .
        /// </summary>
        public class AuthorizeRolesAttribute : AuthorizeAttribute
        {
            /// <summary>
            /// Initialize a new <see cref="AuthorizeRolesAttribute"/> with a list of roles.
            /// </summary>
            /// <param name="roles">Roles to authorize</param>
            public AuthorizeRolesAttribute(params string[] roles) : base()
            {
                Roles = string.Join(",", roles);
            }
        }
    
}