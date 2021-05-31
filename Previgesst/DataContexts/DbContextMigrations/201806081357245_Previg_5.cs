namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Documents", "ApplicationPrevi_ApplicationPreviId", "dbo.ApplicationPrevis");
            DropIndex("dbo.Documents", new[] { "ApplicationPrevi_ApplicationPreviId" });
            DropColumn("dbo.Documents", "ApplicationPrevi_ApplicationPreviId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Documents", "ApplicationPrevi_ApplicationPreviId", c => c.Int());
            CreateIndex("dbo.Documents", "ApplicationPrevi_ApplicationPreviId");
            AddForeignKey("dbo.Documents", "ApplicationPrevi_ApplicationPreviId", "dbo.ApplicationPrevis", "ApplicationPreviId");
        }
    }
}
