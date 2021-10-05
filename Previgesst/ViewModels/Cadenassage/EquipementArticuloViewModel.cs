using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Previgesst.Ressources.Cadenassage;

namespace Previgesst.ViewModels
{
    public class EquipementArticuloViewModel
    {
        public int EquipementArticuloID { get; set; }

        public int EquipementId { get; set; }


        [Required(ErrorMessageResourceType = typeof(CadEquipRES), ErrorMessageResourceName = "ErreurNoAccessories")]
        public int Accessories { get; set; }


        [Required(ErrorMessageResourceType = typeof(CadEquipRES), ErrorMessageResourceName = "ErreurNoEnergy")]
        public int Energy { get; set; }


        [Required(ErrorMessageResourceType = typeof(CadEquipRES), ErrorMessageResourceName = "ErreurNoDeposit")]
        public int Deposit { get; set; }

        public string Nomenclature { get; set; }


        public string AccessoriesDescription { get; set; }

        public string EnergyDescription { get; set; }

        public string DepositDescription { get; set; }

    }
}