using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Previgesst.ViewModels
{
    public class ClientAccessVM
    {
        public Boolean AccessDocuments { get; set; }
        public Boolean AccessCadenassage { get; set; }
        public Boolean AccessAnalyse { get; set; }

        public Boolean AccessUtilisateurs { get; set; }

    }
}