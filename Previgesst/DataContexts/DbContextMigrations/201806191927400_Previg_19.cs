namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_19 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DocumentClients", "ClientId", c => c.Int(nullable: false));
            CreateIndex("dbo.DocumentClients", "ClientId");
            AddForeignKey("dbo.DocumentClients", "ClientId", "dbo.Clients", "ClientId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DocumentClients", "ClientId", "dbo.Clients");
            DropIndex("dbo.DocumentClients", new[] { "ClientId" });
            DropColumn("dbo.DocumentClients", "ClientId");
        }
    }
}
