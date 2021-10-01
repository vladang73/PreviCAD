namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_82 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SavedInstructions", "LigneRegistreId", c => c.Int(nullable: false));
            AddColumn("dbo.SavedInstructionNotes", "LigneRegistreId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SavedInstructionNotes", "LigneRegistreId");
            DropColumn("dbo.SavedInstructions", "LigneRegistreId");
        }
    }
}
