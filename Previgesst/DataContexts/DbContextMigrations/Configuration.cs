using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using Previgesst.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Previgesst.DataContexts.Extensions;

namespace Previgesst.DataContexts.DbContextMigrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"DataContexts\DbContextMigrations";

            //Générateur SQL avec plus de fonctionnalités
            SetSqlGenerator("System.Data.SqlClient", new CustomSqlServerMigrationSqlGenerator());
        }

        //Attention: Faire en sorte que le seed puisse s'exécuter plusieurs fois sans impacte négative sur la BD
        protected override void Seed(AppDbContext context)
        {
            ////Déboggage de la méthode seed dans un autre Visual Studio
            //if(!System.Diagnostics.Debugger.IsAttached)
            //{
            //    System.Diagnostics.Debugger.Launch();
            //}



            ////Roles
            var roleAdminId = "DF0ED225-8424-4FD2-B0B0-B035C7202302";

            var roleAdmin = new Role
            {
                Id = roleAdminId,
                Name = "Administrateur",
                Description = "Administrateur du système"
            };
            context.Roles.AddOrUpdate(roleAdmin);

            //Utilisateurs
            var UserAdminId = "3F492F1C-6B97-4170-B711-C991D32F89B7";
            var userAdmin = new User
            {
                Id = UserAdminId,
                Inactive = false,
                UserName = "Admin",
                Email = "gabrielgm@cde-solutions.com",
                EmailConfirmed = true,
                PasswordHash = @"AA2/6+oCDg5NhucjlHyfz9EQYGMfLeFBsWgoyKk0cUkJIA8pgYgWrCU9Hpr6J48QBQ==",  // Pa$$w0rd
                PhoneNumberConfirmed = false,
                PhoneNumber = null,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                TwoFactorEnabled = false,
                AccessFailedCount = 0,
                LockoutEnabled = true,
                LockoutEndDateUtc = null
            };
            userAdmin.Roles.Add(new IdentityUserRole { UserId = userAdmin.Id, RoleId = roleAdmin.Id });
            context.Users.AddOrUpdate(userAdmin);


            /*for (int i = 0; i < 500; i++)
            {
                context.Users.AddOrUpdate(
                    new User
                    {
                        Id = Guid.NewGuid().ToString("D"),
                        Inactive = false,
                        UserName = "Test" + i,
                        Email = "cde" + i + "@cde-solutions.com",
                        EmailConfirmed = true,
                        PasswordHash = @"AA2/6+oCDg5NhucjlHyfz9EQYGMfLeFBsWgoyKk0cUkJIA8pgYgWrCU9Hpr6J48QBQ==",  // Pa$$w0rd
                        PhoneNumberConfirmed = false,
                        PhoneNumber = null,
                        SecurityStamp = Guid.NewGuid().ToString("D"),
                        TwoFactorEnabled = false,
                        AccessFailedCount = 0,
                        LockoutEnabled = true,
                        LockoutEndDateUtc = null
                    });
            }*/

            //TODO: Faire le seed de toutes les tables de base ici
            //context.Database.ExecuteSqlCommand("");   //Exécution de requête directe à la BD
            //context.NomDuDbSet.AddOrUpdate(...);      //Ajout de données si inexistantes dans la BD

#if DEBUG
            //TODO: Faire le seed de toutes les données de test ici
#endif
            //Enregistrer les informations à la BD
            base.Seed(context);
        }
    }
}