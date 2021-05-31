using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Previgesst.ActionFilters
{
    public class CheckSession : ActionFilterAttribute
    {


        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            var initialRequest = filterContext.HttpContext.Request.Url.AbsolutePath;

            if (filterContext.HttpContext.Session["utilisateur"] == null)
            {
                var routeDICT = new RouteValueDictionary {
                                        { "action", "Index" },
                                        { "controller", "ClientLogin" },
                                        { "ReturnURL", initialRequest }
                };
                filterContext.Result = new RedirectToRouteResult(routeDICT);
            }
            
        }

    }
}