namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_15 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Utilisateurs", "Salt", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Utilisateurs", "Salt");
        }
    }
}
