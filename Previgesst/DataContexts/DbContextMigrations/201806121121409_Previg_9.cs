namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_9 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Documents", new[] { "SectionId" });
            AlterColumn("dbo.Documents", "Titre", c => c.String(maxLength: 250));
            AlterColumn("dbo.Documents", "Ordre", c => c.Int());
            AlterColumn("dbo.Documents", "SectionId", c => c.Int());
            CreateIndex("dbo.Documents", "SectionId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Documents", new[] { "SectionId" });
            AlterColumn("dbo.Documents", "SectionId", c => c.Int(nullable: false));
            AlterColumn("dbo.Documents", "Ordre", c => c.Int(nullable: false));
            AlterColumn("dbo.Documents", "Titre", c => c.String(nullable: false, maxLength: 250));
            CreateIndex("dbo.Documents", "SectionId");
        }
    }
}
