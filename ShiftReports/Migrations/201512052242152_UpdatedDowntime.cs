namespace ShiftReports.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedDowntime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Production", "Manning", c => c.Int(nullable: false));
            AlterColumn("dbo.Downtime", "Reason", c => c.String(maxLength: 255));
            AlterColumn("dbo.Downtime", "Action", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Downtime", "Action", c => c.String());
            AlterColumn("dbo.Downtime", "Reason", c => c.String());
            DropColumn("dbo.Production", "Manning");
        }
    }
}
