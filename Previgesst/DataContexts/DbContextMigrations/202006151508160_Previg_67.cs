namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_67 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "StatusCadenassage", c => c.Int(nullable: false));
            AddColumn("dbo.Clients", "DateCadenassage", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Clients", "TotalCadenassage", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clients", "TotalCadenassage");
            DropColumn("dbo.Clients", "DateCadenassage");
            DropColumn("dbo.Clients", "StatusCadenassage");
        }
    }
}
