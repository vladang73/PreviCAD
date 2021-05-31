namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_45 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.LigneRegistres", "HeureDebut");
            DropColumn("dbo.LigneRegistres", "HeureFin");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LigneRegistres", "HeureFin", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.LigneRegistres", "HeureDebut", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
    }
}
