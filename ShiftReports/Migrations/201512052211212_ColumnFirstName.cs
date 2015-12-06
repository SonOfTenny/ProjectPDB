namespace ShiftReports.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ColumnFirstName : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.User", name: "FirstMidName", newName: "FirstName");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.User", name: "FirstName", newName: "FirstMidName");
        }
    }
}
