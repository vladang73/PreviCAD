namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_49 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LigneRegistres", "NomAuditeur", c => c.String());
            AddColumn("dbo.LigneRegistres", "Rep1", c => c.Boolean(nullable: false));
            AddColumn("dbo.LigneRegistres", "Rep2", c => c.Boolean(nullable: false));
            AddColumn("dbo.LigneRegistres", "Rep3", c => c.Boolean(nullable: false));
            AddColumn("dbo.LigneRegistres", "Rep4", c => c.Boolean(nullable: false));
            AddColumn("dbo.LigneRegistres", "NoteAudit", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.LigneRegistres", "NoteAudit");
            DropColumn("dbo.LigneRegistres", "Rep4");
            DropColumn("dbo.LigneRegistres", "Rep3");
            DropColumn("dbo.LigneRegistres", "Rep2");
            DropColumn("dbo.LigneRegistres", "Rep1");
            DropColumn("dbo.LigneRegistres", "NomAuditeur");
        }
    }
}
