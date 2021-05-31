using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Previgesst.Helpers
{
    public static class IndiceHelper
    {
        public static string getText(int indice)
        {
            if (indice < 7)
                return "--";
            if (indice <= 9)
                return "S";
            if (indice <= 14)
                return "1";
            if (indice <= 24)
                return "2";
            if (indice <= 35)
                return "3";
            if (indice <= 44)
                return "4";
            return "5";
        }

    }
}