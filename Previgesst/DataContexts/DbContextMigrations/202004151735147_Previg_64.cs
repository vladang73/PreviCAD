namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_64 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AnalyseRisques", "DateMiseAJour", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AnalyseRisques", "DateMiseAJour", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
    }
}
