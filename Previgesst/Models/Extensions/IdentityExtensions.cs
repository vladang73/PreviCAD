using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using Previgesst.Enums;

namespace Previgesst.Models.Extensions
{
    /// <summary>
    /// Extensions ajoutée à l'identité (User)
    /// </summary>
    public static class IdentityExtensions
    {
        //Exemple :
        //public static string GetUserFullName(this IIdentity identity)
        //{
        //    var claim = ((ClaimsIdentity)identity).FindFirst(ClaimDefinition.UserFullName);
        //    return (claim != null) ? claim.Value : string.Empty;
        //}
    }
}