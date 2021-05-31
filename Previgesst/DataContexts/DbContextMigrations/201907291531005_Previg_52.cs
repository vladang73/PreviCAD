namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_52 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Departements", "NomDepartementEN", c => c.String(nullable: false, maxLength: 250));
            AddColumn("dbo.Equipements", "NomEquipementEN", c => c.String(nullable: false, maxLength: 250));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Equipements", "NomEquipementEN");
            DropColumn("dbo.Departements", "NomDepartementEN");
        }
    }
}
