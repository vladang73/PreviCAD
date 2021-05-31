namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_31 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.LigneAnalyseRisques", "Rang", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LigneAnalyseRisques", "Rang", c => c.Int(nullable: false));
        }
    }
}
