namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_63 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DommagePossibles",
                c => new
                    {
                        DommagePossibleId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 250),
                        DescriptionEN = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.DommagePossibleId);
            
            AddColumn("dbo.LigneAnalyseRisques", "DommagePossibleId", c => c.Int());
            CreateIndex("dbo.LigneAnalyseRisques", "DommagePossibleId");
            AddForeignKey("dbo.LigneAnalyseRisques", "DommagePossibleId", "dbo.DommagePossibles", "DommagePossibleId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LigneAnalyseRisques", "DommagePossibleId", "dbo.DommagePossibles");
            DropIndex("dbo.LigneAnalyseRisques", new[] { "DommagePossibleId" });
            DropColumn("dbo.LigneAnalyseRisques", "DommagePossibleId");
            DropTable("dbo.DommagePossibles");
        }
    }
}
