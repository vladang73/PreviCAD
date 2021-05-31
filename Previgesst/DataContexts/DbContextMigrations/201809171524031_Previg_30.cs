namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_30 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Frequences", "Valeur", c => c.Int());
            AddColumn("dbo.Gravites", "Valeur", c => c.Int());
            AddColumn("dbo.Possibilites", "Valeur", c => c.Int());
            AddColumn("dbo.Probabilites", "Valeur", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Probabilites", "Valeur");
            DropColumn("dbo.Possibilites", "Valeur");
            DropColumn("dbo.Gravites", "Valeur");
            DropColumn("dbo.Frequences", "Valeur");
        }
    }
}
