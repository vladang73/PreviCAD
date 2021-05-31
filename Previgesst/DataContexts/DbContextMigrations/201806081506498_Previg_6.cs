namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_6 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Equipements",
                c => new
                    {
                        EquipementId = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        NomEquipement = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.EquipementId)
                .ForeignKey("dbo.Clients", t => t.ClientId)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.FicheCadenassages",
                c => new
                    {
                        FicheCadenassageId = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        EquipementId = c.Int(nullable: false),
                        TravailAEffectuer = c.String(nullable: false, maxLength: 250),
                        Batiment = c.String(nullable: false, maxLength: 250),
                        NoFiche = c.String(nullable: false, maxLength: 250),
                        ApprouvePar = c.String(nullable: false, maxLength: 250),
                        DateCreation = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DateRevision = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.FicheCadenassageId)
                .ForeignKey("dbo.Clients", t => t.ClientId)
                .ForeignKey("dbo.Equipements", t => t.EquipementId)
                .Index(t => t.ClientId)
                .Index(t => t.EquipementId);
            
            CreateTable(
                "dbo.LigneDecadenassages",
                c => new
                    {
                        LigneDecadenassageId = c.Int(nullable: false, identity: true),
                        FicheCadenassageId = c.Int(nullable: false),
                        NoLigne = c.Int(nullable: false),
                        texteInstruction = c.String(nullable: false, maxLength: 250),
                        Realiser = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.LigneDecadenassageId)
                .ForeignKey("dbo.FicheCadenassages", t => t.FicheCadenassageId)
                .Index(t => t.FicheCadenassageId);
            
            CreateTable(
                "dbo.LigneInstructions",
                c => new
                    {
                        LigneInstructionId = c.Int(nullable: false, identity: true),
                        FicheCadenassageId = c.Int(nullable: false),
                        NoLigne = c.Int(nullable: false),
                        InstructionId = c.Int(nullable: false),
                        TexteSupplementaireInstruction = c.String(nullable: false, maxLength: 250),
                        TexteSupplementaireDispositif = c.String(nullable: false, maxLength: 250),
                        TexteLocalisation = c.String(nullable: false, maxLength: 250),
                        CocherColonneCadenas = c.Boolean(nullable: false),
                        Realiser = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.LigneInstructionId)
                .ForeignKey("dbo.FicheCadenassages", t => t.FicheCadenassageId)
                .ForeignKey("dbo.Instructions", t => t.InstructionId)
                .Index(t => t.FicheCadenassageId)
                .Index(t => t.InstructionId);
            
            CreateTable(
                "dbo.Instructions",
                c => new
                    {
                        InstructionId = c.Int(nullable: false, identity: true),
                        TexteInstruction = c.String(nullable: false, maxLength: 500),
                        DispositifId = c.Int(nullable: false),
                        AccessoireId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.InstructionId)
                .ForeignKey("dbo.Accessoires", t => t.AccessoireId)
                .ForeignKey("dbo.Dispositifs", t => t.DispositifId)
                .Index(t => t.DispositifId)
                .Index(t => t.AccessoireId);
            
            CreateTable(
                "dbo.MaterielRequisCadenassages",
                c => new
                    {
                        MaterielRequisCadenassageId = c.Int(nullable: false, identity: true),
                        FicheCadenassageId = c.Int(nullable: false),
                        MaterielId = c.Int(nullable: false),
                        Quantite = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MaterielRequisCadenassageId)
                .ForeignKey("dbo.FicheCadenassages", t => t.FicheCadenassageId)
                .ForeignKey("dbo.Materiels", t => t.MaterielId)
                .Index(t => t.FicheCadenassageId)
                .Index(t => t.MaterielId);
            
            CreateTable(
                "dbo.SourceEnergieCadenassages",
                c => new
                    {
                        SourceEnergieCadenassageId = c.Int(nullable: false, identity: true),
                        FicheCadenassageId = c.Int(nullable: false),
                        SourceEnergieId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SourceEnergieCadenassageId)
                .ForeignKey("dbo.FicheCadenassages", t => t.FicheCadenassageId)
                .ForeignKey("dbo.SourceEnergies", t => t.SourceEnergieId)
                .Index(t => t.FicheCadenassageId)
                .Index(t => t.SourceEnergieId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SourceEnergieCadenassages", "SourceEnergieId", "dbo.SourceEnergies");
            DropForeignKey("dbo.SourceEnergieCadenassages", "FicheCadenassageId", "dbo.FicheCadenassages");
            DropForeignKey("dbo.MaterielRequisCadenassages", "MaterielId", "dbo.Materiels");
            DropForeignKey("dbo.MaterielRequisCadenassages", "FicheCadenassageId", "dbo.FicheCadenassages");
            DropForeignKey("dbo.LigneInstructions", "InstructionId", "dbo.Instructions");
            DropForeignKey("dbo.Instructions", "DispositifId", "dbo.Dispositifs");
            DropForeignKey("dbo.Instructions", "AccessoireId", "dbo.Accessoires");
            DropForeignKey("dbo.LigneInstructions", "FicheCadenassageId", "dbo.FicheCadenassages");
            DropForeignKey("dbo.LigneDecadenassages", "FicheCadenassageId", "dbo.FicheCadenassages");
            DropForeignKey("dbo.FicheCadenassages", "EquipementId", "dbo.Equipements");
            DropForeignKey("dbo.FicheCadenassages", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.Equipements", "ClientId", "dbo.Clients");
            DropIndex("dbo.SourceEnergieCadenassages", new[] { "SourceEnergieId" });
            DropIndex("dbo.SourceEnergieCadenassages", new[] { "FicheCadenassageId" });
            DropIndex("dbo.MaterielRequisCadenassages", new[] { "MaterielId" });
            DropIndex("dbo.MaterielRequisCadenassages", new[] { "FicheCadenassageId" });
            DropIndex("dbo.Instructions", new[] { "AccessoireId" });
            DropIndex("dbo.Instructions", new[] { "DispositifId" });
            DropIndex("dbo.LigneInstructions", new[] { "InstructionId" });
            DropIndex("dbo.LigneInstructions", new[] { "FicheCadenassageId" });
            DropIndex("dbo.LigneDecadenassages", new[] { "FicheCadenassageId" });
            DropIndex("dbo.FicheCadenassages", new[] { "EquipementId" });
            DropIndex("dbo.FicheCadenassages", new[] { "ClientId" });
            DropIndex("dbo.Equipements", new[] { "ClientId" });
            DropTable("dbo.SourceEnergieCadenassages");
            DropTable("dbo.MaterielRequisCadenassages");
            DropTable("dbo.Instructions");
            DropTable("dbo.LigneInstructions");
            DropTable("dbo.LigneDecadenassages");
            DropTable("dbo.FicheCadenassages");
            DropTable("dbo.Equipements");
        }
    }
}
