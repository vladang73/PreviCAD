using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Previgesst.Models
{
    public class User : IdentityUser
    {
        public User() : base() { }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Ajouter les revendications personnalisées de l’utilisateur ici 
            //userIdentity.AddClaim(new Claim(ClaimDefinition.NomCompletUtilisateur, MonNomCompletUtilisateur));

            return userIdentity;
        }

        //INFORMATION: Modifier le modèle de l'utilisateur ici.
        [Required]
        public bool Inactive { get; set; }
        public static object Identity { get; internal set; }
    }

    public class Role : IdentityRole
    {
        public Role() : base() { }
        
        [Required]
        [MaxLength(256)]
        public string Description { get; set; }
    }
}