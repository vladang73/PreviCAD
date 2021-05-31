namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_4 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Documents", new[] { "ApplicationPreviId" });
            RenameColumn(table: "dbo.Documents", name: "ApplicationPreviId", newName: "ApplicationPrevi_ApplicationPreviId");
            CreateTable(
                "dbo.Sections",
                c => new
                    {
                        SectionId = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        ApplicationPreviId = c.Int(nullable: false),
                        Ordre = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SectionId)
                .ForeignKey("dbo.ApplicationPrevis", t => t.ApplicationPreviId)
                .Index(t => t.ApplicationPreviId);
            
            AddColumn("dbo.Documents", "SectionId", c => c.Int(nullable: false));
            AlterColumn("dbo.Documents", "ApplicationPrevi_ApplicationPreviId", c => c.Int());
            CreateIndex("dbo.Documents", "SectionId");
            CreateIndex("dbo.Documents", "ApplicationPrevi_ApplicationPreviId");
            AddForeignKey("dbo.Documents", "SectionId", "dbo.Sections", "SectionId");
            DropColumn("dbo.Documents", "Section");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Documents", "Section", c => c.String(nullable: false, maxLength: 250));
            DropForeignKey("dbo.Documents", "SectionId", "dbo.Sections");
            DropForeignKey("dbo.Sections", "ApplicationPreviId", "dbo.ApplicationPrevis");
            DropIndex("dbo.Sections", new[] { "ApplicationPreviId" });
            DropIndex("dbo.Documents", new[] { "ApplicationPrevi_ApplicationPreviId" });
            DropIndex("dbo.Documents", new[] { "SectionId" });
            AlterColumn("dbo.Documents", "ApplicationPrevi_ApplicationPreviId", c => c.Int(nullable: false));
            DropColumn("dbo.Documents", "SectionId");
            DropTable("dbo.Sections");
            RenameColumn(table: "dbo.Documents", name: "ApplicationPrevi_ApplicationPreviId", newName: "ApplicationPreviId");
            CreateIndex("dbo.Documents", "ApplicationPreviId");
        }
    }
}
