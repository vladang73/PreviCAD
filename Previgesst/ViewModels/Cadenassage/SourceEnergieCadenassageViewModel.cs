using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Previgesst.Ressources.Cadenassage;

namespace Previgesst.ViewModels
{
    public class SourceEnergieCadenassageViewModel
    {
        public int  FicheCadenassageId { get; set; }

        [Display(ResourceType = typeof(CadSourcesRES), Name = "Sources")]

        public IEnumerable<int> SourcesEnergieId { get; set; } = new List<int>();

    }
}