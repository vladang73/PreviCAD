namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_46 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "NbAdminsMax", c => c.Int(nullable: false));
            AddColumn("dbo.Clients", "NbAuditeursMax", c => c.Int(nullable: false));
            AddColumn("dbo.Clients", "NbUtilisateursPrevicad", c => c.Int(nullable: false));
            AddColumn("dbo.Utilisateurs", "AdmPrevicad", c => c.Boolean(nullable: false));
            AddColumn("dbo.Utilisateurs", "AdmAnalyseRisque", c => c.Boolean(nullable: false));
            AddColumn("dbo.Utilisateurs", "AdmDocuments", c => c.Boolean(nullable: false));
            AddColumn("dbo.Utilisateurs", "ROPrevicad", c => c.Boolean(nullable: false));
            AddColumn("dbo.Utilisateurs", "ROAnalyseRisque", c => c.Boolean(nullable: false));
            AddColumn("dbo.Utilisateurs", "RODocuments", c => c.Boolean(nullable: false));
            AddColumn("dbo.Utilisateurs", "Auditeur", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Utilisateurs", "Auditeur");
            DropColumn("dbo.Utilisateurs", "RODocuments");
            DropColumn("dbo.Utilisateurs", "ROAnalyseRisque");
            DropColumn("dbo.Utilisateurs", "ROPrevicad");
            DropColumn("dbo.Utilisateurs", "AdmDocuments");
            DropColumn("dbo.Utilisateurs", "AdmAnalyseRisque");
            DropColumn("dbo.Utilisateurs", "AdmPrevicad");
            DropColumn("dbo.Clients", "NbUtilisateursPrevicad");
            DropColumn("dbo.Clients", "NbAuditeursMax");
            DropColumn("dbo.Clients", "NbAdminsMax");
        }
    }
}
