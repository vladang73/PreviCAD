namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_40 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmployeRegistres", "ClientId", c => c.Int(nullable: false));
            CreateIndex("dbo.EmployeRegistres", "ClientId");
            AddForeignKey("dbo.EmployeRegistres", "ClientId", "dbo.Clients", "ClientId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmployeRegistres", "ClientId", "dbo.Clients");
            DropIndex("dbo.EmployeRegistres", new[] { "ClientId" });
            DropColumn("dbo.EmployeRegistres", "ClientId");
        }
    }
}
