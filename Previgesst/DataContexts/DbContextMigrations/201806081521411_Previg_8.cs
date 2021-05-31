namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_8 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LigneInstructions", "AccessoireId", c => c.Int(nullable: false));
            CreateIndex("dbo.LigneInstructions", "AccessoireId");
            AddForeignKey("dbo.LigneInstructions", "AccessoireId", "dbo.Accessoires", "AccessoireId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LigneInstructions", "AccessoireId", "dbo.Accessoires");
            DropIndex("dbo.LigneInstructions", new[] { "AccessoireId" });
            DropColumn("dbo.LigneInstructions", "AccessoireId");
        }
    }
}
