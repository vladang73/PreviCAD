using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Previgesst.Models
{
    public class ApplicationPrevi
    {
        public int ApplicationPreviId { get; set; }
     
        [Required]
        [StringLength(250)]
        public string Nom { get; set; }

        public virtual ICollection<Section> Sections { get; set; }

        public virtual ICollection<ClientApplicationPrevi>ClientsApplicationPrevi { get; set; }

    }
}