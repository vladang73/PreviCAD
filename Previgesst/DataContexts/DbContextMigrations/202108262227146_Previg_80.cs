namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_80 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Utilisateurs", "NotificationNonConformite", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Utilisateurs", "NotificationNonConformite");
        }
    }
}
