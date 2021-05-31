using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Previgesst.DataContexts;
using Previgesst.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;
using Owin;

namespace Previgesst
{
    public class IoCConfig
    {
        public static void RegisterDependencies(IAppBuilder app)
        {
            var builder = new ContainerBuilder();
            
            //Enregistrer les Repositories
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                   .Where(t => t.Name.EndsWith("Repository", StringComparison.InvariantCultureIgnoreCase))
                   //.AsImplementedInterfaces()
                   .InstancePerRequest();

            //Enregistrer les Services
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                   .Where(t => t.Name.EndsWith("Service", StringComparison.InvariantCultureIgnoreCase))
                   //.AsImplementedInterfaces()
                   .InstancePerRequest();
            
            //Enregistrer le DbContext
            builder.Register(c => AppDbContext.Create()).As<DbContext>().InstancePerRequest();

            //Enregistrer le UserManager et ses dépendances
            builder.RegisterType<UserStore>().As<IUserStore<User>>().InstancePerRequest();
            builder.RegisterType<UserManager>().AsSelf().InstancePerRequest();
            builder.Register<IDataProtectionProvider>(c => app.GetDataProtectionProvider()).InstancePerRequest();

            //Enregistrer le SignInManager et ses dépendances (UserManager fait parti des dépendences)
            builder.RegisterType<SignInManager>().AsSelf().InstancePerRequest();
            builder.Register<IAuthenticationManager>(c => HttpContext.Current.GetOwinContext().Authentication).InstancePerRequest();

            //Enregistrer le RoleManager et ses dépendances
            builder.RegisterType<RoleStore>().As<IRoleStore<Role, string>>().InstancePerRequest();
            builder.RegisterType<RoleManager>().AsSelf().InstancePerRequest();

            //Enregistrer les controller
            builder.RegisterControllers(typeof(MvcApplication).Assembly)
                   .InstancePerRequest();

            //Enregistrer le logger
            builder.RegisterModule<NLogModule>();
                        
            //Permettre le DI dans les filtres
            builder.RegisterFilterProvider();

            //Construire les dépendences
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            app.UseAutofacMiddleware(container);
            app.UseAutofacMvc();
        }
    }
}