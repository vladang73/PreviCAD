namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_38 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "IdentificateurUnique", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clients", "IdentificateurUnique");
        }
    }
}
