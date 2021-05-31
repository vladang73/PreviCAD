namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_26 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LigneAnalyseRisques", "ReglementId", "dbo.Reglements");
            DropIndex("dbo.LigneAnalyseRisques", new[] { "ReglementId" });
            AddColumn("dbo.LigneAnalyseRisques", "ReglesEtNormes", c => c.String());
            DropColumn("dbo.LigneAnalyseRisques", "ReglementId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LigneAnalyseRisques", "ReglementId", c => c.Int());
            DropColumn("dbo.LigneAnalyseRisques", "ReglesEtNormes");
            CreateIndex("dbo.LigneAnalyseRisques", "ReglementId");
            AddForeignKey("dbo.LigneAnalyseRisques", "ReglementId", "dbo.Reglements", "ReglementId");
        }
    }
}
