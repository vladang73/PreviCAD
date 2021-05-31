using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Previgesst.ViewModels
{
    public class ClientListViewModel
    {
        public int ClientId { get; set; }

        [Required]
        [StringLength(250)]
        [DisplayName("Nom du client")]
        public string Nom { get; set; }


        [Required]
        [StringLength(250)]
        [DisplayName("Identificateur")]
        public string Identificateur { get; set; }



        public Boolean Actif { get; set; }

        public Boolean EstSupprimable { get; set; }

        public string Bidon { get; set; }

        public string Logo { get; set; }

        public string Thumb { get; set; }

        public Guid? UniqueIdentifier { get; set; }

        public string Lien { get; set; }
    }
}