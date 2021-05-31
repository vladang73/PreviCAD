namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_56 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PhotoFicheCadenassages", "LocalisationEN", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PhotoFicheCadenassages", "LocalisationEN");
        }
    }
}
