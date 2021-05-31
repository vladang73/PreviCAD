namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_461 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "NbAdminsPrevicadMax", c => c.Int(nullable: false));
            AddColumn("dbo.Clients", "NbAdminsAnalyseRisqueMax", c => c.Int(nullable: false));
            AddColumn("dbo.Clients", "NbAdminsDocumentsMax", c => c.Int(nullable: false));
            DropColumn("dbo.Clients", "NbAdminsMax");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Clients", "NbAdminsMax", c => c.Int(nullable: false));
            DropColumn("dbo.Clients", "NbAdminsDocumentsMax");
            DropColumn("dbo.Clients", "NbAdminsAnalyseRisqueMax");
            DropColumn("dbo.Clients", "NbAdminsPrevicadMax");
        }
    }
}
