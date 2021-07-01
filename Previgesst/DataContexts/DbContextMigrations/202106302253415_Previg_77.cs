namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_77 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DocumentFicheNotes",
                c => new
                    {
                        FicheCadenassageId = c.Int(nullable: false),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.FicheCadenassageId)
                .ForeignKey("dbo.FicheCadenassages", t => t.FicheCadenassageId)
                .Index(t => t.FicheCadenassageId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DocumentFicheNotes", "FicheCadenassageId", "dbo.FicheCadenassages");
            DropIndex("dbo.DocumentFicheNotes", new[] { "FicheCadenassageId" });
            DropTable("dbo.DocumentFicheNotes");
        }
    }
}
