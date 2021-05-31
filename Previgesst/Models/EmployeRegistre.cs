using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Previgesst.Models
{
    public class EmployeRegistre
    {

        public int EmployeRegistreId { get; set; }


        [Required]
        [StringLength(250)]
        public string Nom { get; set; }


        public int ClientId { get; set; }
        public virtual Client Client { get; set; }

        public int DepartementId { get; set; }
        public virtual Departement Departement { get; set; }

        [Required]
        [StringLength(25)]
        public string NoEmploye { get; set; }

        [Required]
        [StringLength(250)]
        public string Password { get; set; }


        [StringLength(250)]
        public string NoCadenas { get; set; }

        public virtual ICollection<LigneRegistre> LignesRegistre { get; set; }

        public bool Actif { get; set; }


    }
}