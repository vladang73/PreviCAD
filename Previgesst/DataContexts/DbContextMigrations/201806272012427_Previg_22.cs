namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_22 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClientApplicationPrevis",
                c => new
                    {
                        ClientApplicationPreviId = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        ApplicationPreviId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ClientApplicationPreviId)
                .ForeignKey("dbo.ApplicationPrevis", t => t.ApplicationPreviId)
                .ForeignKey("dbo.Clients", t => t.ClientId)
                .Index(t => t.ClientId)
                .Index(t => t.ApplicationPreviId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClientApplicationPrevis", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.ClientApplicationPrevis", "ApplicationPreviId", "dbo.ApplicationPrevis");
            DropIndex("dbo.ClientApplicationPrevis", new[] { "ApplicationPreviId" });
            DropIndex("dbo.ClientApplicationPrevis", new[] { "ClientId" });
            DropTable("dbo.ClientApplicationPrevis");
        }
    }
}
