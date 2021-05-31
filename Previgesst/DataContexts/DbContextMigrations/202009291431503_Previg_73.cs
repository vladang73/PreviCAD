namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_73 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LigneRegistres", "BonDeTravail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.LigneRegistres", "BonDeTravail");
        }
    }
}
