namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_72 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ParametresApps",
                c => new
                    {
                        ParametresAppId = c.Int(nullable: false, identity: true),
                        NextUpdateCase = c.Boolean(nullable: false),
                        NextUpdateTextFr = c.String(),
                        NextUpdateTextEn = c.String(),
                    })
                .PrimaryKey(t => t.ParametresAppId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ParametresApps");
        }
    }
}
