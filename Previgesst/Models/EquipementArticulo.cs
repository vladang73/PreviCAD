using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Previgesst.Models
{
    public class EquipementArticulo
    {
        public int EquipementArticuloID { get; set; }

        public int EquipementId { get; set; }

        public virtual Equipement Equipement { get; set; }

        public int Accessories { get; set; }

        public int Energy { get; set; }

        public int Deposit { get; set; }

        public string Nomenclature { get; set; }
    }
}