namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_71 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "NbLimitCadenassage", c => c.Int(nullable: false));
            AddColumn("dbo.Clients", "PeriodeEssai", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clients", "PeriodeEssai");
            DropColumn("dbo.Clients", "NbLimitCadenassage");
        }
    }
}
