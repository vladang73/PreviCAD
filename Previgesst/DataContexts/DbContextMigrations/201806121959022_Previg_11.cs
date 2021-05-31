namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "Logo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clients", "Logo");
        }
    }
}
