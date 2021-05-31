namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_55 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FicheCadenassages", "TravailAEffectuerEN", c => c.String(nullable: false, maxLength: 250));
            AddColumn("dbo.FicheCadenassages", "TitreFicheEN", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FicheCadenassages", "TitreFicheEN");
            DropColumn("dbo.FicheCadenassages", "TravailAEffectuerEN");
        }
    }
}
