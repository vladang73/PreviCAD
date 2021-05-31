namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_25 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FicheCadenassages", "AfficherClient", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FicheCadenassages", "AfficherClient");
        }
    }
}
