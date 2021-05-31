namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RoleUtilisateurs",
                c => new
                    {
                        RoleUtilisateurId = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                    })
                .PrimaryKey(t => t.RoleUtilisateurId);
            
            CreateTable(
                "dbo.Utilisateurs",
                c => new
                    {
                        UtilisateurId = c.Int(nullable: false, identity: true),
                        NomUtilisateur = c.String(),
                        PasswordHash = c.String(),
                        Nom = c.String(),
                        RoleUtilisateurId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UtilisateurId)
                .ForeignKey("dbo.RoleUtilisateurs", t => t.RoleUtilisateurId)
                .Index(t => t.RoleUtilisateurId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Utilisateurs", "RoleUtilisateurId", "dbo.RoleUtilisateurs");
            DropIndex("dbo.Utilisateurs", new[] { "RoleUtilisateurId" });
            DropTable("dbo.Utilisateurs");
            DropTable("dbo.RoleUtilisateurs");
        }
    }
}
