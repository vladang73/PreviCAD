using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Previgesst.Models
{
    public class DocumentClient
    {
        public int DocumentClientId { get; set; }


        public int ClientId { get; set; }
    
        public virtual Client Client { get; set; }


        [StringLength(250)]
        public string Titre { get; set; }

        public int? Ordre { get; set; }


        [StringLength(250)]
        public string FileName { get; set; }

        public int? ApplicationPreviId { get; set; }

        public virtual ApplicationPrevi ApplicationPrevi { get; set; }
    }
}