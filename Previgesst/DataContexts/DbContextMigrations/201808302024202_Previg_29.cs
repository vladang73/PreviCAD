namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_29 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LigneAnalyseRisques", "ConformiteAuxNormes", c => c.Boolean(nullable: false));
            DropColumn("dbo.LigneAnalyseRisques", "ConformiteAuNormes");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LigneAnalyseRisques", "ConformiteAuNormes", c => c.String(maxLength: 250));
            DropColumn("dbo.LigneAnalyseRisques", "ConformiteAuxNormes");
        }
    }
}
