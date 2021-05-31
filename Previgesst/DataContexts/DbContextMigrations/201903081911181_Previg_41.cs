namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_41 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.LigneRegistres", "DateFin", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.LigneRegistres", "HeureFin", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LigneRegistres", "HeureFin", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.LigneRegistres", "DateFin", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
    }
}
