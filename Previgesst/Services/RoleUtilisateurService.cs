using Previgesst.Repositories;
using Previgesst.ViewModels.DDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Previgesst.Services
{
    public class RoleUtilisateurService
    {
        RoleUtilisateurRepository roleUtilisateurRepository;
        public RoleUtilisateurService(RoleUtilisateurRepository roleUtilisateurRepository)
        {
            this.roleUtilisateurRepository = roleUtilisateurRepository;
        }


        public IEnumerable<RoleUtilisateurDDLViewModel> GetAllAsRolesUtilisateursDDLViewModel()
        {
            var situationDDL = roleUtilisateurRepository.AsQueryable().Where(x=>x.Nom != "System").Select(s => new RoleUtilisateurDDLViewModel()
            {
               Nom   = s.Nom,
               RoleUtilisateurId = s.RoleUtilisateurId
            }).OrderBy(s => s.RoleUtilisateurId);

            return situationDDL;
        }
    }



}