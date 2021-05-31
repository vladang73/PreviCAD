namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_36 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.LigneInstructions", "TexteLocalisation");
            DropColumn("dbo.LigneDecadenassages", "TexteLocalisation");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LigneDecadenassages", "TexteLocalisation", c => c.String(maxLength: 250));
            AddColumn("dbo.LigneInstructions", "TexteLocalisation", c => c.String(maxLength: 250));
        }
    }
}
