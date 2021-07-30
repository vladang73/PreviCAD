namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_79 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Equipements", "QRCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Equipements", "QRCode");
        }
    }
}
