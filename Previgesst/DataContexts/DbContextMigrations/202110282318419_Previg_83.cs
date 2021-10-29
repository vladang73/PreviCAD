namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_83 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FicheCadenassages", "DateValidated", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.FicheCadenassages", "ValidatedPar", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FicheCadenassages", "ValidatedPar");
            DropColumn("dbo.FicheCadenassages", "DateValidated");
        }
    }
}
