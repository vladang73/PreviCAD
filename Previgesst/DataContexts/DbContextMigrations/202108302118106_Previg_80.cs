namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_80 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SavedInstructions",
                c => new
                    {
                        PKId = c.Int(nullable: false, identity: true),
                        InstructionId = c.Int(nullable: false),
                        InstructionType = c.String(nullable: false, maxLength: 50),
                        StepStatus = c.String(nullable: false),
                        FicheCadenassageId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PKId);
            
            AddColumn("dbo.Utilisateurs", "NotificationNonConformite", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Utilisateurs", "NotificationNonConformite");
            DropTable("dbo.SavedInstructions");
        }
    }
}
