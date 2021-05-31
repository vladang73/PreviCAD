namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_61 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.LigneAnalyseRisques", "NoReference");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LigneAnalyseRisques", "NoReference", c => c.String(nullable: false, maxLength: 250));
        }
    }
}
