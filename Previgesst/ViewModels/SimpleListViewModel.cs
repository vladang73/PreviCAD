using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Previgesst.Ressources;

namespace Previgesst.ViewModels
{
    public class SimpleListViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        [MaxLength(250, ErrorMessageResourceType = typeof(ListesRES), ErrorMessageResourceName = "DescriptionMaxLength")]
        [Display(ResourceType = typeof(ListesRES), Name = "Description")]
        public string Description { get; set; }


        [Required]
        [StringLength(250)]
        [Display(ResourceType = typeof(ListesRES), Name = "DescriptionEN")]
        [MaxLength(250, ErrorMessageResourceType = typeof(ListesRES), ErrorMessageResourceName = "DescriptionMaxLength")]
        public string DescriptionEN { get; set; }


        public bool Suppressible { get; set; }
    }
}