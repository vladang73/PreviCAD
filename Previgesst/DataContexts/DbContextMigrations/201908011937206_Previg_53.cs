namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_53 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LigneInstructions", "TexteSupplementaireInstructionEN", c => c.String(maxLength: 250));
            AddColumn("dbo.LigneInstructions", "TexteSupplementaireDispositifEN", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LigneInstructions", "TexteSupplementaireDispositifEN");
            DropColumn("dbo.LigneInstructions", "TexteSupplementaireInstructionEN");
        }
    }
}
