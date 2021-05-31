namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_501 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Instructions", "TexteInstructionEN", c => c.String());
            AddColumn("dbo.Instructions", "IdentificateurEN", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Instructions", "IdentificateurEN");
            DropColumn("dbo.Instructions", "TexteInstructionEN");
        }
    }
}
