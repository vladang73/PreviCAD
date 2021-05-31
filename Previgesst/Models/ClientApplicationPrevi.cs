using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Previgesst.Models
{
    public class ClientApplicationPrevi
    {
        public int ClientApplicationPreviId { get; set; }


        public int ClientId { get; set; }
        public virtual Client Client { get; set; }

        public int ApplicationPreviId { get; set; }
        public virtual ApplicationPrevi ApplicationPrevi { get; set; }


    }
}