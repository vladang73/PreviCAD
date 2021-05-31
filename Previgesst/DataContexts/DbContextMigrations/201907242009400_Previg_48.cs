namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_48 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "NbAdminUtilisateurs", c => c.Int(nullable: false));
            AddColumn("dbo.Utilisateurs", "AdmUtilisateurs", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Utilisateurs", "AdmUtilisateurs");
            DropColumn("dbo.Clients", "NbAdminUtilisateurs");
        }
    }
}
