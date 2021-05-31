namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_16 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Utilisateurs", "Password", c => c.String(nullable: false, maxLength: 500));
            AddColumn("dbo.Utilisateurs", "Courriel", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("dbo.Utilisateurs", "NomUtilisateur", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("dbo.Utilisateurs", "Nom", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.Utilisateurs", "PasswordHash");
            DropColumn("dbo.Utilisateurs", "Salt");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Utilisateurs", "Salt", c => c.String());
            AddColumn("dbo.Utilisateurs", "PasswordHash", c => c.String());
            AlterColumn("dbo.Utilisateurs", "Nom", c => c.String());
            AlterColumn("dbo.Utilisateurs", "NomUtilisateur", c => c.String());
            DropColumn("dbo.Utilisateurs", "Courriel");
            DropColumn("dbo.Utilisateurs", "Password");
        }
    }
}
