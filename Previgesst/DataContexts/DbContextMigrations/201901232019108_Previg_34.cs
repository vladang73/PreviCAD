namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_34 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.LigneInstructions", "NomFichier");
            DropColumn("dbo.LigneDecadenassages", "NomFichier");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LigneDecadenassages", "NomFichier", c => c.String(maxLength: 500));
            AddColumn("dbo.LigneInstructions", "NomFichier", c => c.String(maxLength: 500));
        }
    }
}
