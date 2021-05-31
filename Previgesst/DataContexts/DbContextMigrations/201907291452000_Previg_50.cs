namespace Previgesst.DataContexts.DbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Previg_50 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accessoires", "DescriptionEN", c => c.String(nullable: false, maxLength: 250));
            AddColumn("dbo.Dispositifs", "DescriptionEN", c => c.String(nullable: false, maxLength: 250));
            AddColumn("dbo.Evenements", "DescriptionEN", c => c.String(nullable: false, maxLength: 250));
            AddColumn("dbo.Phenomenes", "DescriptionEN", c => c.String(nullable: false, maxLength: 250));
            AddColumn("dbo.Situations", "DescriptionEN", c => c.String(nullable: false, maxLength: 250));
            AddColumn("dbo.Materiels", "DescriptionEN", c => c.String(nullable: false, maxLength: 250));
            AddColumn("dbo.SourceEnergies", "DescriptionEN", c => c.String(nullable: false, maxLength: 250));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SourceEnergies", "DescriptionEN");
            DropColumn("dbo.Materiels", "DescriptionEN");
            DropColumn("dbo.Situations", "DescriptionEN");
            DropColumn("dbo.Phenomenes", "DescriptionEN");
            DropColumn("dbo.Evenements", "DescriptionEN");
            DropColumn("dbo.Dispositifs", "DescriptionEN");
            DropColumn("dbo.Accessoires", "DescriptionEN");
        }
    }
}
