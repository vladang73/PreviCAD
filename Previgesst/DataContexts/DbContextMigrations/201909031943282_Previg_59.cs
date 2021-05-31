namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_59 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AnalyseRisques", "Equipement", c => c.String(nullable: false, maxLength: 250));
            DropColumn("dbo.LigneAnalyseRisques", "Equipement");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LigneAnalyseRisques", "Equipement", c => c.String(nullable: false, maxLength: 250));
            DropColumn("dbo.AnalyseRisques", "Equipement");
        }
    }
}
