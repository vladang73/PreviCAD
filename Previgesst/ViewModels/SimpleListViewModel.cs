using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Previgesst.ViewModels
{
    public class SimpleListViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(250)]
       
        public  string Description { get; set; }

        [Required]
        [StringLength(250)]
       
        public string DescriptionEN { get; set; }

        public bool Suppressible { get; set; }

    }
}