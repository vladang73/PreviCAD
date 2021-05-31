namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_57 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Utilisateurs", "AdmDocuments");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Utilisateurs", "AdmDocuments", c => c.Boolean(nullable: false));
        }
    }
}
