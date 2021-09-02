namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_81 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SavedInstructionNotes",
                c => new
                    {
                        PKId = c.Int(nullable: false, identity: true),
                        Notes = c.String(nullable: false),
                        FicheCadenassageId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PKId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SavedInstructionNotes");
        }
    }
}
