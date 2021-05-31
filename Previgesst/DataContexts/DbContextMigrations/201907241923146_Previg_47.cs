namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_47 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Utilisateurs", new[] { "RoleUtilisateurId" });
            RenameColumn(table: "dbo.Utilisateurs", name: "RoleUtilisateurId", newName: "RoleUtilisateur_RoleUtilisateurId");
            AlterColumn("dbo.Utilisateurs", "RoleUtilisateur_RoleUtilisateurId", c => c.Int());
            CreateIndex("dbo.Utilisateurs", "RoleUtilisateur_RoleUtilisateurId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Utilisateurs", new[] { "RoleUtilisateur_RoleUtilisateurId" });
            AlterColumn("dbo.Utilisateurs", "RoleUtilisateur_RoleUtilisateurId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Utilisateurs", name: "RoleUtilisateur_RoleUtilisateurId", newName: "RoleUtilisateurId");
            CreateIndex("dbo.Utilisateurs", "RoleUtilisateurId");
        }
    }
}
