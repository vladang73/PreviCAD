using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Previgesst.Models
{
    public class Document
    {
        public int DocumentId { get; set; }

        
        [StringLength(250)]
        public string Titre { get; set; }
      
        public int? Ordre { get; set; }

      
        [StringLength(250)]
        public string FileName { get; set; }

        public int? SectionId { get; set; }

        public virtual Section Section { get; set; }
    }
}