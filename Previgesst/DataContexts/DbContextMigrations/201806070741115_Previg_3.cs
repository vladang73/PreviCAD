namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Utilisateurs", "Actif", c => c.Boolean(nullable: false));
            AddColumn("dbo.Utilisateurs", "ClientId", c => c.Int(nullable: false));
            CreateIndex("dbo.Utilisateurs", "ClientId");
            AddForeignKey("dbo.Utilisateurs", "ClientId", "dbo.Clients", "ClientId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Utilisateurs", "ClientId", "dbo.Clients");
            DropIndex("dbo.Utilisateurs", new[] { "ClientId" });
            DropColumn("dbo.Utilisateurs", "ClientId");
            DropColumn("dbo.Utilisateurs", "Actif");
        }
    }
}
