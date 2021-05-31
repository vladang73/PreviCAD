namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_44 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LigneRegistres", "FinPrevue", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LigneRegistres", "FinPrevue");
        }
    }
}
