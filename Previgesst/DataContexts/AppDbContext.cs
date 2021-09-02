using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using Previgesst.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Configuration;

namespace Previgesst.DataContexts
{
    public class AppDbContext : IdentityDbContext<User>
    {
        //DbSets de l'application

        public DbSet<DommagePossible> DommagesPossibles { get; set; }
        public DbSet<EmployeRegistre> EmployesRegistre { get; set; }

        public DbSet<LigneRegistre> LignesRegistre { get; set; }
        public DbSet<PhotoFicheCadenassage> PhotosFichersCadenassage { get; set; }
        public DbSet<ClientApplicationPrevi> ClientApplicationPrevi { get; set; }
        public DbSet<DocumentClient> DocumentClient { get; set; }
        public DbSet<Departement> Departement { get; set; }
        public DbSet<FicheCadenassage> FicheCadenassage { get; set; }
        public DbSet<LigneInstruction> LigneInstruction { get; set; }
        public DbSet<LigneDecadenassage> LigneDecadenassage { get; set; }
        public DbSet<MaterielRequisCadenassage> MaterielRequisCadenassage { get; set; }

        public DbSet<SourceEnergieCadenassage> SourceEnergieCadenassage { get; set; }

        public DbSet<Instruction> Instruction { get; set; }



        public DbSet<Equipement> Equipement { get; set; }

        public DbSet<Section> Section { get; set; }
        public DbSet<Utilisateur> Utilisateur { get; set; }
        public DbSet<RoleUtilisateur> RoleUtilisateur { get; set; }

        public DbSet<ApplicationPrevi> ApplicationsPrevi { get; set; }
        public DbSet<Document> Documents { get; set; }

        public DbSet<Cadenas> Cadenass { get; set; }
        public DbSet<LogEntry> LogEntries { get; set; }
        public DbSet<ParametresApp> ParametresApp { get; set; }
        public DbSet<Phenomene> Phenomene { get; set; }
        public DbSet<Situation> Situation { get; set; }
        public DbSet<Evenement> Evenement { get; set; }
        public DbSet<TypeReduction> TypeReduction { get; set; }


        public DbSet<Dispositif> Dispositif { get; set; }
        public DbSet<Accessoire> Accessoire { get; set; }
        public DbSet<SourceEnergie> SourceEnergie { get; set; }
        public DbSet<Materiel> Materiel { get; set; }

        public DbSet<Gravite> Gravite { get; set; }
        public DbSet<Frequence> Frequence { get; set; }
        public DbSet<Probabilite> Probabilite { get; set; }
        public DbSet<Possibilite> Possibilite { get; set; }
        public DbSet<Client> Client { get; set; }

        public DbSet<AnalyseRisque> AnalyseRisque { get; set; }

        public DbSet<LigneAnalyseRisque> LigneAnalyseRisque { get; set; }

        public DbSet<Reglement> Reglement { get; set; }

        public DbSet<DocumentFiche> DocumentFiche { get; set; }

        public DbSet<DocumentFicheNote> DocumentFicheNote { get; set; }
        
        public DbSet<SavedInstruction> SavedInstruction { get; set; }
        public DbSet<SavedInstructionNote> SavedInstructionNote { get; set; }

        /// <summary>
        /// Constructeur qui appel la base (DbContext) avec la connection string en paramêtre.
        /// </summary>
        /// <param name="connectionString">Connection string requise.</param>
        private AppDbContext(string connectionString) : base(connectionString) { }

        /// <summary>
        /// Créer une nouvelle instance du PartenairesDbContext
        /// </summary>
        /// <returns>Le DbContext</returns>
        public static AppDbContext Create()
        {
            var dbContext = new AppDbContext(WebConfigurationManager.ConnectionStrings["ConnexionSQL"].ConnectionString);

            return dbContext;
        }

        /// <summary>
        /// Méthodes pour faire des changement sur la création des models. Ne pas renommer.
        /// </summary>
        /// <remarks>Est utilisé pour ajouter ou enlever des Conventions, ou faire du Fluent-API</remarks>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Pour enlever les cascades:
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            //On force les propriété de type C# "DateTime" d'être des "datetime2" en SQL.
            //La raison est que un DateTime C# non-nullable a une valeure par défaut qui va
            //en dessous de la limite du datetime SQL normal et cause une exception.
            modelBuilder.Properties<DateTime>()
                        .Configure(c => c.HasColumnType("datetime2"));
        }
    }

    /// <summary>
    /// Factory pour créer le AppDbContext
    /// </summary>
    /// <remarks>Utilisé pour les migrations d'entity framework</remarks>
    public class AppDbContextFactory : IDbContextFactory<AppDbContext>
    {
        public AppDbContext Create()
        {
            return AppDbContext.Create();
        }
    }
}