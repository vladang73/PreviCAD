namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_35 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PhotoFicheCadenassages", "Localisation", c => c.String());
            AddColumn("dbo.PhotoFicheCadenassages", "Fichier", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PhotoFicheCadenassages", "Fichier");
            DropColumn("dbo.PhotoFicheCadenassages", "Localisation");
        }
    }
}
