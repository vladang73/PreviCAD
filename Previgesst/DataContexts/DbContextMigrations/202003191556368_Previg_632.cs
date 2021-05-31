namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_632 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AnalyseRisques", "DateCreation", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AnalyseRisques", "DateCreation");
        }
    }
}
