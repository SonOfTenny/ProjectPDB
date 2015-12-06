namespace ShiftReports.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedDowntime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Downtime", "Reason", c => c.String(maxLength: 255));
            AlterColumn("dbo.Downtime", "Action", c => c.String(maxLength: 255));
            AddForeignKey("dbo.Downtime", "DowntimeTypeID", "dbo.DowntimeType", "DowntimeTypeID", cascadeDelete: true);
            CreateIndex("dbo.Downtime", "DowntimeTypeID");
            DropColumn("dbo.Downtime", "DowntimeType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Downtime", "DowntimeType", c => c.String());
            DropIndex("dbo.Downtime", new[] { "DowntimeTypeID" });
            DropForeignKey("dbo.Downtime", "DowntimeTypeID", "dbo.DowntimeType");
            AlterColumn("dbo.Downtime", "Action", c => c.String());
            AlterColumn("dbo.Downtime", "Reason", c => c.String());
        }
        
    }
}
