namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_32 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AnalyseRisques", "estDocumentPrevigesst", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AnalyseRisques", "estDocumentPrevigesst");
        }
    }
}
