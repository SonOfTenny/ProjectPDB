namespace ShiftReports.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedProduction : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Production", "TargetMix");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Production", "TargetMix", c => c.Int(nullable: false));
        }
    }
}
