namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FicheCadenassages", "Departement", c => c.String(nullable: false, maxLength: 250));
            DropColumn("dbo.FicheCadenassages", "Batiment");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FicheCadenassages", "Batiment", c => c.String(nullable: false, maxLength: 250));
            DropColumn("dbo.FicheCadenassages", "Departement");
        }
    }
}
