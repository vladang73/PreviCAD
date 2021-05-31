using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Previgesst.Models
{
    public class Section
    { public int SectionId {get;set;}
        public string Nom { get; set; }
        public int ApplicationPreviId { get; set; }

        public virtual ApplicationPrevi ApplicationPrevi { get; set; }

        public int Ordre { get; set; }

        public virtual ICollection<Document> Document { get; set; }

    }
}