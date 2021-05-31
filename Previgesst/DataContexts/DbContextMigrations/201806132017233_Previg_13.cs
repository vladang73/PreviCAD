namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_13 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LigneInstructions", "Equipement_EquipementId", c => c.Int());
            AddColumn("dbo.Equipements", "NomFichier", c => c.String(maxLength: 250));
            CreateIndex("dbo.LigneInstructions", "Equipement_EquipementId");
            AddForeignKey("dbo.LigneInstructions", "Equipement_EquipementId", "dbo.Equipements", "EquipementId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LigneInstructions", "Equipement_EquipementId", "dbo.Equipements");
            DropIndex("dbo.LigneInstructions", new[] { "Equipement_EquipementId" });
            DropColumn("dbo.Equipements", "NomFichier");
            DropColumn("dbo.LigneInstructions", "Equipement_EquipementId");
        }
    }
}
