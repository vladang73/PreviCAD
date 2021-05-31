namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_12 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "Thumb", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clients", "Thumb");
        }
    }
}
