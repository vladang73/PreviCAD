using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using NLog;

namespace Previgesst.ActionFilters
{
    //Source : http://stackoverflow.com/a/1318059/888479
    //Exemple d'utilisation sur méthode : 
    // [Throttle(Name="TestThrottle", Message = "Attendez {n} secondes avant de réessayer cette action.", Seconds = 5)]

    /// <summary>
    /// Decorates any MVC route that needs to have client requests limited by time.
    /// </summary>
    /// <remarks>
    /// Uses the current System.Web.Caching.Cache to store each client request to the decorated route.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class ThrottleAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Logger pour garder une trace au cas où un arrête la demande.
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// A unique name for this Throttle.
        /// </summary>
        /// <remarks>
        /// We'll be inserting a Cache record based on this name and client IP, e.g. "Name-192.168.0.1"
        /// </remarks>
        public string Name { get; set; }

        /// <summary>
        /// The number of seconds clients must wait before executing this decorated route again.
        /// </summary>
        public int Seconds { get; set; }

        /// <summary>
        /// A text message that will be sent to the client upon throttling.  You can include the token {n} to
        /// show this.Seconds in the message, e.g. "Wait {n} seconds before trying again".
        /// </summary>
        public string Message { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var key = string.Concat(Name, "-", filterContext.HttpContext.Request.UserHostAddress);
            var allowExecute = false;

            if (HttpRuntime.Cache[key] == null)
            {
                HttpRuntime.Cache.Add(key,
                    true, // is this the smallest data we can have?
                    null, // no dependencies
                    DateTime.Now.AddSeconds(Seconds), // absolute expiration
                    Cache.NoSlidingExpiration,
                    CacheItemPriority.Low,
                    null); // no callback

                allowExecute = true;
            }

            if (!allowExecute)
            {
                if (String.IsNullOrEmpty(Message))
                    Message = "Vous devez attendre {n} secondes pour effectuer cette action de nouveau.";

                filterContext.Result = new ContentResult { Content = Message.Replace("{n}", Seconds.ToString()) };
                // see 409 - http://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html
                filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Conflict;

                if (Logger != null) Logger.Info("Trop de demande pour la requête suivante : " + key);
            }
        }


    }
}