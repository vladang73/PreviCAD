namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_28 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LigneAnalyseRisques", "MesureId", "dbo.Mesures");
            DropIndex("dbo.LigneAnalyseRisques", new[] { "MesureId" });
            AddColumn("dbo.LigneAnalyseRisques", "MesurePrevention", c => c.String(maxLength: 500));
            DropColumn("dbo.LigneAnalyseRisques", "MesureId");
            DropTable("dbo.Mesures");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Mesures",
                c => new
                    {
                        MesureId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.MesureId);
            
            AddColumn("dbo.LigneAnalyseRisques", "MesureId", c => c.Int());
            DropColumn("dbo.LigneAnalyseRisques", "MesurePrevention");
            CreateIndex("dbo.LigneAnalyseRisques", "MesureId");
            AddForeignKey("dbo.LigneAnalyseRisques", "MesureId", "dbo.Mesures", "MesureId");
        }
    }
}
