namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_631 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AnalyseRisques", "UserMAJ", c => c.String(maxLength: 250));
            AddColumn("dbo.AnalyseRisques", "Createur", c => c.String(nullable: false, maxLength: 250));
            AddColumn("dbo.AnalyseRisques", "Participants", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AnalyseRisques", "Participants");
            DropColumn("dbo.AnalyseRisques", "Createur");
            DropColumn("dbo.AnalyseRisques", "UserMAJ");
        }
    }
}
