namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_54 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LigneDecadenassages", "TexteSupplementaireInstructionEN", c => c.String(maxLength: 250));
            AddColumn("dbo.LigneDecadenassages", "TexteSupplementaireDispositifEN", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LigneDecadenassages", "TexteSupplementaireDispositifEN");
            DropColumn("dbo.LigneDecadenassages", "TexteSupplementaireInstructionEN");
        }
    }
}
