namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_76 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DocumentFiches",
                c => new
                    {
                        DocumentFicheId = c.Int(nullable: false, identity: true),
                        Titre = c.String(maxLength: 250),
                        Ordre = c.Int(),
                        FileName = c.String(maxLength: 250),
                        ApplicationPreviId = c.Int(),
                        FicheCadenassageId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DocumentFicheId)
                .ForeignKey("dbo.ApplicationPrevis", t => t.ApplicationPreviId)
                .ForeignKey("dbo.FicheCadenassages", t => t.FicheCadenassageId)
                .Index(t => t.ApplicationPreviId)
                .Index(t => t.FicheCadenassageId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DocumentFiches", "FicheCadenassageId", "dbo.FicheCadenassages");
            DropForeignKey("dbo.DocumentFiches", "ApplicationPreviId", "dbo.ApplicationPrevis");
            DropIndex("dbo.DocumentFiches", new[] { "FicheCadenassageId" });
            DropIndex("dbo.DocumentFiches", new[] { "ApplicationPreviId" });
            DropTable("dbo.DocumentFiches");
        }
    }
}
