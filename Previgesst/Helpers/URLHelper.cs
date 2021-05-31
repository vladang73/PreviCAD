using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace Previgesst.Helpers
{
    public class URLHelper
    {

        public static string GetBaseUrl()
        {
            var request = HttpContext.Current.Request;
            var appUrl = HttpRuntime.AppDomainAppVirtualPath;

            if (appUrl.Left(1) != "/")
                appUrl = "/" + appUrl;

       

            var baseUrl = string.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, appUrl);


            if (baseUrl.Right(1) != "/" && baseUrl.Right(1) != @"\")
                baseUrl += "/";

            return baseUrl;
        }


    }
}