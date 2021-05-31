namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_14 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LigneInstructions", "Equipement_EquipementId", "dbo.Equipements");
            DropIndex("dbo.LigneInstructions", new[] { "Equipement_EquipementId" });
            DropColumn("dbo.LigneInstructions", "Equipement_EquipementId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LigneInstructions", "Equipement_EquipementId", c => c.Int());
            CreateIndex("dbo.LigneInstructions", "Equipement_EquipementId");
            AddForeignKey("dbo.LigneInstructions", "Equipement_EquipementId", "dbo.Equipements", "EquipementId");
        }
    }
}
