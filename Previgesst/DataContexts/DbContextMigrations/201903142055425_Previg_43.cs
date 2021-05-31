namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_43 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LigneRegistres", "EquipementId", "dbo.Equipements");
            DropIndex("dbo.LigneRegistres", new[] { "EquipementId" });
            AddColumn("dbo.LigneRegistres", "FicheCadenassageId", c => c.Int(nullable: false));
            CreateIndex("dbo.LigneRegistres", "FicheCadenassageId");
            AddForeignKey("dbo.LigneRegistres", "FicheCadenassageId", "dbo.FicheCadenassages", "FicheCadenassageId");
            DropColumn("dbo.LigneRegistres", "EquipementId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LigneRegistres", "EquipementId", c => c.Int(nullable: false));
            DropForeignKey("dbo.LigneRegistres", "FicheCadenassageId", "dbo.FicheCadenassages");
            DropIndex("dbo.LigneRegistres", new[] { "FicheCadenassageId" });
            DropColumn("dbo.LigneRegistres", "FicheCadenassageId");
            CreateIndex("dbo.LigneRegistres", "EquipementId");
            AddForeignKey("dbo.LigneRegistres", "EquipementId", "dbo.Equipements", "EquipementId");
        }
    }
}
