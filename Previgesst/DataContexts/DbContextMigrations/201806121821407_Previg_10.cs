namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Instructions", "Identificateur", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Instructions", "Identificateur");
        }
    }
}
