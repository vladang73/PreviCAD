namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_18 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departements",
                c => new
                    {
                        DepartementId = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        NomDepartement = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.DepartementId)
                .ForeignKey("dbo.Clients", t => t.ClientId)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.DocumentClients",
                c => new
                    {
                        DocumentClientId = c.Int(nullable: false, identity: true),
                        Titre = c.String(maxLength: 250),
                        Ordre = c.Int(),
                        FileName = c.String(maxLength: 250),
                        ApplicationPreviId = c.Int(),
                    })
                .PrimaryKey(t => t.DocumentClientId)
                .ForeignKey("dbo.ApplicationPrevis", t => t.ApplicationPreviId)
                .Index(t => t.ApplicationPreviId);
            
            AddColumn("dbo.LigneInstructions", "TexteSupplementaireRealiser", c => c.String());
            AddColumn("dbo.FicheCadenassages", "DepartementId", c => c.Int(nullable: false));
            AddColumn("dbo.FicheCadenassages", "estDocumentPrevigesst", c => c.Boolean(nullable: false));
            AddColumn("dbo.LigneAnalyseRisques", "estDocumentPrevigesst", c => c.Boolean(nullable: false));
            AddColumn("dbo.LigneDecadenassages", "InstructionId", c => c.Int());
            AddColumn("dbo.LigneDecadenassages", "TexteSupplementaireInstruction", c => c.String(maxLength: 250));
            AddColumn("dbo.LigneDecadenassages", "TexteSupplementaireDispositif", c => c.String(maxLength: 250));
            AddColumn("dbo.LigneDecadenassages", "TexteLocalisation", c => c.String(maxLength: 250));
            AddColumn("dbo.LigneDecadenassages", "CocherColonneCadenas", c => c.Boolean(nullable: false));
            AddColumn("dbo.LigneDecadenassages", "NomFichier", c => c.String(maxLength: 500));
            AddColumn("dbo.LigneDecadenassages", "TexteSupplementaireRealiser", c => c.String());
            AlterColumn("dbo.LigneDecadenassages", "NoLigne", c => c.Int());
            CreateIndex("dbo.FicheCadenassages", "DepartementId");
            CreateIndex("dbo.LigneDecadenassages", "InstructionId");
            AddForeignKey("dbo.FicheCadenassages", "DepartementId", "dbo.Departements", "DepartementId");
            AddForeignKey("dbo.LigneDecadenassages", "InstructionId", "dbo.Instructions", "InstructionId");
            DropColumn("dbo.FicheCadenassages", "Departement");
            DropColumn("dbo.LigneDecadenassages", "texteInstruction");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LigneDecadenassages", "texteInstruction", c => c.String(nullable: false, maxLength: 250));
            AddColumn("dbo.FicheCadenassages", "Departement", c => c.String(nullable: false, maxLength: 250));
            DropForeignKey("dbo.DocumentClients", "ApplicationPreviId", "dbo.ApplicationPrevis");
            DropForeignKey("dbo.LigneDecadenassages", "InstructionId", "dbo.Instructions");
            DropForeignKey("dbo.FicheCadenassages", "DepartementId", "dbo.Departements");
            DropForeignKey("dbo.Departements", "ClientId", "dbo.Clients");
            DropIndex("dbo.DocumentClients", new[] { "ApplicationPreviId" });
            DropIndex("dbo.LigneDecadenassages", new[] { "InstructionId" });
            DropIndex("dbo.Departements", new[] { "ClientId" });
            DropIndex("dbo.FicheCadenassages", new[] { "DepartementId" });
            AlterColumn("dbo.LigneDecadenassages", "NoLigne", c => c.Int(nullable: false));
            DropColumn("dbo.LigneDecadenassages", "TexteSupplementaireRealiser");
            DropColumn("dbo.LigneDecadenassages", "NomFichier");
            DropColumn("dbo.LigneDecadenassages", "CocherColonneCadenas");
            DropColumn("dbo.LigneDecadenassages", "TexteLocalisation");
            DropColumn("dbo.LigneDecadenassages", "TexteSupplementaireDispositif");
            DropColumn("dbo.LigneDecadenassages", "TexteSupplementaireInstruction");
            DropColumn("dbo.LigneDecadenassages", "InstructionId");
            DropColumn("dbo.LigneAnalyseRisques", "estDocumentPrevigesst");
            DropColumn("dbo.FicheCadenassages", "estDocumentPrevigesst");
            DropColumn("dbo.FicheCadenassages", "DepartementId");
            DropColumn("dbo.LigneInstructions", "TexteSupplementaireRealiser");
            DropTable("dbo.DocumentClients");
            DropTable("dbo.Departements");
        }
    }
}
