using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Previgesst.Models
{
    public class Departement
    {
        public int DepartementId { get; set; }
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }

        [Required]
        [StringLength(250)]
        public string NomDepartement { get; set; }


        [Required]
        [StringLength(250)]
        public string NomDepartementEN { get; set; }


        public virtual ICollection<FicheCadenassage> FicheCadenassage { get; set; }
        public virtual ICollection<EmployeRegistre>EmployesRegistres { get; set; }
    }
}