namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_23 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FicheCadenassages", "TitreFiche", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FicheCadenassages", "TitreFiche");
        }
    }
}
