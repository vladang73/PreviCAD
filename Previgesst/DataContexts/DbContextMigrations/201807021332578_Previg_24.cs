namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_24 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.LigneInstructions", "NoLigne", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.LigneDecadenassages", "NoLigne", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LigneDecadenassages", "NoLigne", c => c.Int());
            AlterColumn("dbo.LigneInstructions", "NoLigne", c => c.Int());
        }
    }
}
