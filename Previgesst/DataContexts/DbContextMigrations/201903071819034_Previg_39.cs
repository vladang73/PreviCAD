namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_39 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.EmployeRegistres", "NoCadenas", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EmployeRegistres", "NoCadenas", c => c.String(nullable: false, maxLength: 250));
        }
    }
}
