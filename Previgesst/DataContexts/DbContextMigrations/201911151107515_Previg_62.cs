namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_62 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FicheCadenassages", "TravailAEffectuerEN", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FicheCadenassages", "TravailAEffectuerEN", c => c.String(nullable: false, maxLength: 250));
        }
    }
}
