namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_74 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Utilisateurs", "NotificationDebutCad", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Utilisateurs", "NotificationDebutCad");
        }
    }
}
