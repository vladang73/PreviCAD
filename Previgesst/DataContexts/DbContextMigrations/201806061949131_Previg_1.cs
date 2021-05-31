namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "Identificateur", c => c.String(nullable: false, maxLength: 250));
            AddColumn("dbo.Clients", "Actif", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Clients", "Nom", c => c.String(nullable: false, maxLength: 250));
            CreateIndex("dbo.Clients", "Identificateur", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Clients", new[] { "Identificateur" });
            AlterColumn("dbo.Clients", "Nom", c => c.String());
            DropColumn("dbo.Clients", "Actif");
            DropColumn("dbo.Clients", "Identificateur");
        }
    }
}
