namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_58 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LigneAnalyseRisques", "PlanAction", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LigneAnalyseRisques", "PlanAction");
        }
    }
}
