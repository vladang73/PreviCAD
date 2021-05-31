namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_68 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LigneRegistres", "LuEtEffectue", c => c.Int(nullable: false));
            AddColumn("dbo.LigneRegistres", "LuEtDecadenasse", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LigneRegistres", "LuEtDecadenasse");
            DropColumn("dbo.LigneRegistres", "LuEtEffectue");
        }
    }
}
