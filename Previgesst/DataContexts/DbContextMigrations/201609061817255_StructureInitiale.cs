namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StructureInitiale : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accessoires",
                c => new
                    {
                        AccessoireId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.AccessoireId);
            
            CreateTable(
                "dbo.AnalyseRisques",
                c => new
                    {
                        AnalyseRisqueId = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        Description = c.String(nullable: false, maxLength: 250),
                        DateMiseAJour = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        AfficherChezClient = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AnalyseRisqueId)
                .ForeignKey("dbo.Clients", t => t.ClientId)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ClientId = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                    })
                .PrimaryKey(t => t.ClientId);
            
            CreateTable(
                "dbo.LigneAnalyseRisques",
                c => new
                    {
                        LigneAnalyseRisqueId = c.Int(nullable: false, identity: true),
                        AnalyseRisqueId = c.Int(nullable: false),
                        NoReference = c.String(nullable: false, maxLength: 250),
                        Rang = c.Int(nullable: false),
                        Equipement = c.String(nullable: false, maxLength: 250),
                        Operation = c.String(nullable: false, maxLength: 250),
                        Zone = c.String(nullable: false, maxLength: 250),
                        PhenomeneId = c.Int(),
                        SituationId = c.Int(),
                        EvenementId = c.Int(),
                        GraviteAvantId = c.Int(),
                        FrequenceAvantId = c.Int(),
                        ProbabiliteAvantId = c.Int(),
                        PossibiliteAvantId = c.Int(),
                        IndiceFinalAvant = c.Int(nullable: false),
                        NbPersonnesExposees = c.Int(nullable: false),
                        SystemeCommandeAvant = c.String(maxLength: 250),
                        ReglementId = c.Int(),
                        MesureId = c.Int(),
                        GraviteApresId = c.Int(),
                        FrequenceApresId = c.Int(),
                        ProbabiliteApresId = c.Int(),
                        PossibiliteApresId = c.Int(),
                        IndiceFinalApres = c.Int(nullable: false),
                        SystemeCommandeInstalles = c.String(maxLength: 250),
                        ConformiteAuNormes = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.LigneAnalyseRisqueId)
                .ForeignKey("dbo.AnalyseRisques", t => t.AnalyseRisqueId)
                .ForeignKey("dbo.Evenements", t => t.EvenementId)
                .ForeignKey("dbo.Frequences", t => t.FrequenceApresId)
                .ForeignKey("dbo.Frequences", t => t.FrequenceAvantId)
                .ForeignKey("dbo.Gravites", t => t.GraviteApresId)
                .ForeignKey("dbo.Gravites", t => t.GraviteAvantId)
                .ForeignKey("dbo.Mesures", t => t.MesureId)
                .ForeignKey("dbo.Phenomenes", t => t.PhenomeneId)
                .ForeignKey("dbo.Possibilites", t => t.PossibiliteApresId)
                .ForeignKey("dbo.Possibilites", t => t.PossibiliteAvantId)
                .ForeignKey("dbo.Probabilites", t => t.ProbabiliteApresId)
                .ForeignKey("dbo.Probabilites", t => t.ProbabiliteAvantId)
                .ForeignKey("dbo.Reglements", t => t.ReglementId)
                .ForeignKey("dbo.Situations", t => t.SituationId)
                .Index(t => t.AnalyseRisqueId)
                .Index(t => t.PhenomeneId)
                .Index(t => t.SituationId)
                .Index(t => t.EvenementId)
                .Index(t => t.GraviteAvantId)
                .Index(t => t.FrequenceAvantId)
                .Index(t => t.ProbabiliteAvantId)
                .Index(t => t.PossibiliteAvantId)
                .Index(t => t.ReglementId)
                .Index(t => t.MesureId)
                .Index(t => t.GraviteApresId)
                .Index(t => t.FrequenceApresId)
                .Index(t => t.ProbabiliteApresId)
                .Index(t => t.PossibiliteApresId);
            
            CreateTable(
                "dbo.Evenements",
                c => new
                    {
                        EvenementId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.EvenementId);
            
            CreateTable(
                "dbo.Frequences",
                c => new
                    {
                        FrequenceId = c.Int(nullable: false, identity: true),
                        No = c.Int(nullable: false),
                        Niveau = c.String(nullable: false, maxLength: 5),
                        Explication = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.FrequenceId);
            
            CreateTable(
                "dbo.Gravites",
                c => new
                    {
                        GraviteId = c.Int(nullable: false, identity: true),
                        No = c.Int(nullable: false),
                        Niveau = c.String(nullable: false, maxLength: 5),
                        Explication = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.GraviteId);
            
            CreateTable(
                "dbo.Mesures",
                c => new
                    {
                        MesureId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.MesureId);
            
            CreateTable(
                "dbo.Phenomenes",
                c => new
                    {
                        PhenomeneId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.PhenomeneId);
            
            CreateTable(
                "dbo.Possibilites",
                c => new
                    {
                        PossibiliteId = c.Int(nullable: false, identity: true),
                        No = c.Int(nullable: false),
                        Niveau = c.String(nullable: false, maxLength: 5),
                        Explication = c.String(nullable: false, maxLength: 400),
                    })
                .PrimaryKey(t => t.PossibiliteId);
            
            CreateTable(
                "dbo.Probabilites",
                c => new
                    {
                        ProbabiliteId = c.Int(nullable: false, identity: true),
                        No = c.Int(nullable: false),
                        Niveau = c.String(nullable: false, maxLength: 5),
                        Explication = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.ProbabiliteId);
            
            CreateTable(
                "dbo.Reglements",
                c => new
                    {
                        ReglementId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.ReglementId);
            
            CreateTable(
                "dbo.Situations",
                c => new
                    {
                        SituationId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.SituationId);
            
            CreateTable(
                "dbo.ApplicationPrevis",
                c => new
                    {
                        ApplicationPreviId = c.Int(nullable: false, identity: true),
                        Nom = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.ApplicationPreviId);
            
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        DocumentId = c.Int(nullable: false, identity: true),
                        Titre = c.String(nullable: false, maxLength: 250),
                        Section = c.String(nullable: false, maxLength: 250),
                        Ordre = c.Int(nullable: false),
                        FileName = c.String(maxLength: 250),
                        ApplicationPreviId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DocumentId)
                .ForeignKey("dbo.ApplicationPrevis", t => t.ApplicationPreviId)
                .Index(t => t.ApplicationPreviId);
            
            CreateTable(
                "dbo.Cadenas",
                c => new
                    {
                        CadenasId = c.Int(nullable: false, identity: true),
                        Departement = c.String(),
                        NoFiche = c.String(),
                        Travail = c.String(),
                        Equipement = c.String(),
                    })
                .PrimaryKey(t => t.CadenasId);
            
            CreateTable(
                "dbo.Dispositifs",
                c => new
                    {
                        DispositifId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.DispositifId);
            
            CreateTable(
                "dbo.__LogEntries",
                c => new
                    {
                        LogEntryId = c.Int(nullable: false, identity: true),
                        CallSite = c.String(),
                        Date = c.DateTime(precision: 7, storeType: "datetime2"),
                        Exception = c.String(),
                        Level = c.String(maxLength: 50),
                        Logger = c.String(maxLength: 255),
                        MachineName = c.String(),
                        Message = c.String(),
                        StackTrace = c.String(),
                        Thread = c.String(),
                        Username = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.LogEntryId);
            
            CreateTable(
                "dbo.Materiels",
                c => new
                    {
                        MaterielId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.MaterielId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Description = c.String(maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.SourceEnergies",
                c => new
                    {
                        SourceEnergieId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.SourceEnergieId);
            
            CreateTable(
                "dbo.TypeReductions",
                c => new
                    {
                        TypeReductionId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.TypeReductionId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Inactive = c.Boolean(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(precision: 7, storeType: "datetime2"),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Documents", "ApplicationPreviId", "dbo.ApplicationPrevis");
            DropForeignKey("dbo.LigneAnalyseRisques", "SituationId", "dbo.Situations");
            DropForeignKey("dbo.LigneAnalyseRisques", "ReglementId", "dbo.Reglements");
            DropForeignKey("dbo.LigneAnalyseRisques", "ProbabiliteAvantId", "dbo.Probabilites");
            DropForeignKey("dbo.LigneAnalyseRisques", "ProbabiliteApresId", "dbo.Probabilites");
            DropForeignKey("dbo.LigneAnalyseRisques", "PossibiliteAvantId", "dbo.Possibilites");
            DropForeignKey("dbo.LigneAnalyseRisques", "PossibiliteApresId", "dbo.Possibilites");
            DropForeignKey("dbo.LigneAnalyseRisques", "PhenomeneId", "dbo.Phenomenes");
            DropForeignKey("dbo.LigneAnalyseRisques", "MesureId", "dbo.Mesures");
            DropForeignKey("dbo.LigneAnalyseRisques", "GraviteAvantId", "dbo.Gravites");
            DropForeignKey("dbo.LigneAnalyseRisques", "GraviteApresId", "dbo.Gravites");
            DropForeignKey("dbo.LigneAnalyseRisques", "FrequenceAvantId", "dbo.Frequences");
            DropForeignKey("dbo.LigneAnalyseRisques", "FrequenceApresId", "dbo.Frequences");
            DropForeignKey("dbo.LigneAnalyseRisques", "EvenementId", "dbo.Evenements");
            DropForeignKey("dbo.LigneAnalyseRisques", "AnalyseRisqueId", "dbo.AnalyseRisques");
            DropForeignKey("dbo.AnalyseRisques", "ClientId", "dbo.Clients");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Documents", new[] { "ApplicationPreviId" });
            DropIndex("dbo.LigneAnalyseRisques", new[] { "PossibiliteApresId" });
            DropIndex("dbo.LigneAnalyseRisques", new[] { "ProbabiliteApresId" });
            DropIndex("dbo.LigneAnalyseRisques", new[] { "FrequenceApresId" });
            DropIndex("dbo.LigneAnalyseRisques", new[] { "GraviteApresId" });
            DropIndex("dbo.LigneAnalyseRisques", new[] { "MesureId" });
            DropIndex("dbo.LigneAnalyseRisques", new[] { "ReglementId" });
            DropIndex("dbo.LigneAnalyseRisques", new[] { "PossibiliteAvantId" });
            DropIndex("dbo.LigneAnalyseRisques", new[] { "ProbabiliteAvantId" });
            DropIndex("dbo.LigneAnalyseRisques", new[] { "FrequenceAvantId" });
            DropIndex("dbo.LigneAnalyseRisques", new[] { "GraviteAvantId" });
            DropIndex("dbo.LigneAnalyseRisques", new[] { "EvenementId" });
            DropIndex("dbo.LigneAnalyseRisques", new[] { "SituationId" });
            DropIndex("dbo.LigneAnalyseRisques", new[] { "PhenomeneId" });
            DropIndex("dbo.LigneAnalyseRisques", new[] { "AnalyseRisqueId" });
            DropIndex("dbo.AnalyseRisques", new[] { "ClientId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.TypeReductions");
            DropTable("dbo.SourceEnergies");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Materiels");
            DropTable("dbo.__LogEntries");
            DropTable("dbo.Dispositifs");
            DropTable("dbo.Cadenas");
            DropTable("dbo.Documents");
            DropTable("dbo.ApplicationPrevis");
            DropTable("dbo.Situations");
            DropTable("dbo.Reglements");
            DropTable("dbo.Probabilites");
            DropTable("dbo.Possibilites");
            DropTable("dbo.Phenomenes");
            DropTable("dbo.Mesures");
            DropTable("dbo.Gravites");
            DropTable("dbo.Frequences");
            DropTable("dbo.Evenements");
            DropTable("dbo.LigneAnalyseRisques");
            DropTable("dbo.Clients");
            DropTable("dbo.AnalyseRisques");
            DropTable("dbo.Accessoires");
        }
    }
}
