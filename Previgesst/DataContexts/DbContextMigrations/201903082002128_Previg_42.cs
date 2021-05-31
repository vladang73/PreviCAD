namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_42 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmployeRegistres", "Actif", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EmployeRegistres", "Actif");
        }
    }
}
