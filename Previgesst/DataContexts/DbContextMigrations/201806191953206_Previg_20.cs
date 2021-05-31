namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_20 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FicheCadenassages", "DateCreation", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FicheCadenassages", "DateCreation", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
    }
}
