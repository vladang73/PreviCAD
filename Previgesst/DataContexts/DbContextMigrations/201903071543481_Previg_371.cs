namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_371 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmployeRegistres",
                c => new
                    {
                        EmployeRegistreId = c.Int(nullable: false, identity: true),
                        Nom = c.String(nullable: false, maxLength: 250),
                        DepartementId = c.Int(nullable: false),
                        NoEmploye = c.String(nullable: false, maxLength: 25),
                        Password = c.String(nullable: false, maxLength: 250),
                        NoCadenas = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.EmployeRegistreId)
                .ForeignKey("dbo.Departements", t => t.DepartementId)
                .Index(t => t.DepartementId);
            
            CreateTable(
                "dbo.LigneRegistres",
                c => new
                    {
                        LigneRegistreId = c.Int(nullable: false, identity: true),
                        EquipementId = c.Int(nullable: false),
                        EmployeRegistreId = c.Int(nullable: false),
                        DateDebut = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        HeureDebut = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DateFin = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        HeureFin = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.LigneRegistreId)
                .ForeignKey("dbo.EmployeRegistres", t => t.EmployeRegistreId)
                .ForeignKey("dbo.Equipements", t => t.EquipementId)
                .Index(t => t.EquipementId)
                .Index(t => t.EmployeRegistreId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LigneRegistres", "EquipementId", "dbo.Equipements");
            DropForeignKey("dbo.LigneRegistres", "EmployeRegistreId", "dbo.EmployeRegistres");
            DropForeignKey("dbo.EmployeRegistres", "DepartementId", "dbo.Departements");
            DropIndex("dbo.LigneRegistres", new[] { "EmployeRegistreId" });
            DropIndex("dbo.LigneRegistres", new[] { "EquipementId" });
            DropIndex("dbo.EmployeRegistres", new[] { "DepartementId" });
            DropTable("dbo.LigneRegistres");
            DropTable("dbo.EmployeRegistres");
        }
    }
}
