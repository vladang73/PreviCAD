namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_70 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.LigneAnalyseRisques", "DateDeRealisation", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LigneAnalyseRisques", "DateDeRealisation", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
    }
}
