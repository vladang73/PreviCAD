namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_78 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FicheCadenassages", "IsApproved", c => c.Boolean(nullable: false));
            DropColumn("dbo.FicheCadenassages", "RevisionCourante");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FicheCadenassages", "RevisionCourante", c => c.Boolean(nullable: false));
            DropColumn("dbo.FicheCadenassages", "IsApproved");
        }
    }
}
