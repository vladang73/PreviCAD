using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Previgesst.ViewModels
{
    public class SectionViewModel
    {
        public int SectionId { get; set; }
        [Required]
        public string Nom { get; set; }
        public int Ordre { get; set; }
        [DisplayName("Application")]

        [Required]
        public int ApplicationPreviId { get; set; }


        [DisplayName("Application")]
        public string ApplicationName { get; set; }
        public Boolean Suppressible { get; set; }

    }
}