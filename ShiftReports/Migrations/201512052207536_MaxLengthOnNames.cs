namespace ShiftReports.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MaxLengthOnNames : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.User", "LastName", c => c.String(maxLength: 25));
            AlterColumn("dbo.User", "FirstMidName", c => c.String(maxLength: 25));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.User", "FirstMidName", c => c.String());
            AlterColumn("dbo.User", "LastName", c => c.String());
        }
    }
}
