namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_73 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EquipementArticuloes",
                c => new
                    {
                        EquipementArticuloID = c.Int(nullable: false, identity: true),
                        EquipementId = c.Int(nullable: false),
                        Accessories = c.Int(nullable: false),
                        Energy = c.Int(nullable: false),
                        Deposit = c.Int(nullable: false),
                        Nomenclature = c.String(),
                    })
                .PrimaryKey(t => t.EquipementArticuloID)
                .ForeignKey("dbo.Equipements", t => t.EquipementId)
                .Index(t => t.EquipementId);
            
            AddColumn("dbo.LigneRegistres", "BonDeTravail", c => c.String());
            AddColumn("dbo.Equipements", "NumeroEquipement", c => c.String(nullable: false, maxLength: 250));
            AddColumn("dbo.Equipements", "Model", c => c.String(maxLength: 250));
            AddColumn("dbo.Equipements", "Manufacturer", c => c.String(maxLength: 250));
            AddColumn("dbo.Equipements", "Task", c => c.String());
            AddColumn("dbo.Equipements", "RiskAnalysis", c => c.Boolean());
            AddColumn("dbo.Equipements", "LockoutProcedure", c => c.Boolean());
            AddColumn("dbo.Equipements", "SafeWorkProcedure", c => c.Boolean());
            AddColumn("dbo.Equipements", "NumberOfSerie", c => c.String());
            AddColumn("dbo.Equipements", "YearOfProduction", c => c.Int());
            AddColumn("dbo.Equipements", "Function", c => c.String());
            AddColumn("dbo.Utilisateurs", "NotificationDebutCad", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EquipementArticuloes", "EquipementId", "dbo.Equipements");
            DropIndex("dbo.EquipementArticuloes", new[] { "EquipementId" });
            DropColumn("dbo.Utilisateurs", "NotificationDebutCad");
            DropColumn("dbo.Equipements", "Function");
            DropColumn("dbo.Equipements", "YearOfProduction");
            DropColumn("dbo.Equipements", "NumberOfSerie");
            DropColumn("dbo.Equipements", "SafeWorkProcedure");
            DropColumn("dbo.Equipements", "LockoutProcedure");
            DropColumn("dbo.Equipements", "RiskAnalysis");
            DropColumn("dbo.Equipements", "Task");
            DropColumn("dbo.Equipements", "Manufacturer");
            DropColumn("dbo.Equipements", "Model");
            DropColumn("dbo.Equipements", "NumeroEquipement");
            DropColumn("dbo.LigneRegistres", "BonDeTravail");
            DropTable("dbo.EquipementArticuloes");
        }
    }
}
