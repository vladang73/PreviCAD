namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_69 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Etats",
                c => new
                    {
                        EtatId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 250),
                        DescriptionEN = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.EtatId);
            
            AddColumn("dbo.LigneAnalyseRisques", "ResponsableAnalyse", c => c.String(maxLength: 500));
            AddColumn("dbo.LigneAnalyseRisques", "DateDeRealisation", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.LigneAnalyseRisques", "EtatId", c => c.Int());
            CreateIndex("dbo.LigneAnalyseRisques", "EtatId");
            AddForeignKey("dbo.LigneAnalyseRisques", "EtatId", "dbo.Etats", "EtatId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LigneAnalyseRisques", "EtatId", "dbo.Etats");
            DropIndex("dbo.LigneAnalyseRisques", new[] { "EtatId" });
            DropColumn("dbo.LigneAnalyseRisques", "EtatId");
            DropColumn("dbo.LigneAnalyseRisques", "DateDeRealisation");
            DropColumn("dbo.LigneAnalyseRisques", "ResponsableAnalyse");
            DropTable("dbo.Etats");
        }
    }
}
