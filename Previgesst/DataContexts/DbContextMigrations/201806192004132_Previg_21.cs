namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_21 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FicheCadenassages", "NoFiche", c => c.String(maxLength: 250));
            AlterColumn("dbo.FicheCadenassages", "ApprouvePar", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FicheCadenassages", "ApprouvePar", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("dbo.FicheCadenassages", "NoFiche", c => c.String(nullable: false, maxLength: 250));
        }
    }
}
