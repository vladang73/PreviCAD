namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_17 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LigneInstructions", "AccessoireId", "dbo.Accessoires");
            DropIndex("dbo.LigneInstructions", new[] { "AccessoireId" });
            DropIndex("dbo.LigneInstructions", new[] { "InstructionId" });
            AddColumn("dbo.LigneInstructions", "NomFichier", c => c.String(maxLength: 500));
            AlterColumn("dbo.LigneInstructions", "NoLigne", c => c.Int());
            AlterColumn("dbo.LigneInstructions", "InstructionId", c => c.Int());
            AlterColumn("dbo.LigneInstructions", "TexteSupplementaireInstruction", c => c.String(maxLength: 250));
            AlterColumn("dbo.LigneInstructions", "TexteSupplementaireDispositif", c => c.String(maxLength: 250));
            AlterColumn("dbo.LigneInstructions", "TexteLocalisation", c => c.String(maxLength: 250));
            CreateIndex("dbo.LigneInstructions", "InstructionId");
            DropColumn("dbo.LigneInstructions", "AccessoireId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LigneInstructions", "AccessoireId", c => c.Int(nullable: false));
            DropIndex("dbo.LigneInstructions", new[] { "InstructionId" });
            AlterColumn("dbo.LigneInstructions", "TexteLocalisation", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("dbo.LigneInstructions", "TexteSupplementaireDispositif", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("dbo.LigneInstructions", "TexteSupplementaireInstruction", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("dbo.LigneInstructions", "InstructionId", c => c.Int(nullable: false));
            AlterColumn("dbo.LigneInstructions", "NoLigne", c => c.Int(nullable: false));
            DropColumn("dbo.LigneInstructions", "NomFichier");
            CreateIndex("dbo.LigneInstructions", "InstructionId");
            CreateIndex("dbo.LigneInstructions", "AccessoireId");
            AddForeignKey("dbo.LigneInstructions", "AccessoireId", "dbo.Accessoires", "AccessoireId");
        }
    }
}
