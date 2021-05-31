namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_33 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PhotoFicheCadenassages",
                c => new
                    {
                        PhotoFicheCadenassageId = c.Int(nullable: false, identity: true),
                        Rang = c.Int(nullable: false),
                        FicheCadenassageId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PhotoFicheCadenassageId)
                .ForeignKey("dbo.FicheCadenassages", t => t.FicheCadenassageId)
                .Index(t => t.FicheCadenassageId);
            
            AddColumn("dbo.LigneInstructions", "PhotoFicheCadenassageId", c => c.Int());
            AddColumn("dbo.LigneDecadenassages", "PhotoFicheCadenassageId", c => c.Int());
            CreateIndex("dbo.LigneInstructions", "PhotoFicheCadenassageId");
            CreateIndex("dbo.LigneDecadenassages", "PhotoFicheCadenassageId");
            AddForeignKey("dbo.LigneDecadenassages", "PhotoFicheCadenassageId", "dbo.PhotoFicheCadenassages", "PhotoFicheCadenassageId");
            AddForeignKey("dbo.LigneInstructions", "PhotoFicheCadenassageId", "dbo.PhotoFicheCadenassages", "PhotoFicheCadenassageId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LigneInstructions", "PhotoFicheCadenassageId", "dbo.PhotoFicheCadenassages");
            DropForeignKey("dbo.LigneDecadenassages", "PhotoFicheCadenassageId", "dbo.PhotoFicheCadenassages");
            DropForeignKey("dbo.PhotoFicheCadenassages", "FicheCadenassageId", "dbo.FicheCadenassages");
            DropIndex("dbo.PhotoFicheCadenassages", new[] { "FicheCadenassageId" });
            DropIndex("dbo.LigneDecadenassages", new[] { "PhotoFicheCadenassageId" });
            DropIndex("dbo.LigneInstructions", new[] { "PhotoFicheCadenassageId" });
            DropColumn("dbo.LigneDecadenassages", "PhotoFicheCadenassageId");
            DropColumn("dbo.LigneInstructions", "PhotoFicheCadenassageId");
            DropTable("dbo.PhotoFicheCadenassages");
        }
    }
}
