namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_60 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.LigneRegistres", "Rep1", c => c.String());
            AlterColumn("dbo.LigneRegistres", "Rep2", c => c.String());
            AlterColumn("dbo.LigneRegistres", "Rep3", c => c.String());
            AlterColumn("dbo.LigneRegistres", "Rep4", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LigneRegistres", "Rep4", c => c.Boolean(nullable: false));
            AlterColumn("dbo.LigneRegistres", "Rep3", c => c.Boolean(nullable: false));
            AlterColumn("dbo.LigneRegistres", "Rep2", c => c.Boolean(nullable: false));
            AlterColumn("dbo.LigneRegistres", "Rep1", c => c.Boolean(nullable: false));
        }
    }
}
