namespace ShiftReports.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        LastName = c.String(),
                        FirstMidName = c.String(),
                        joined = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateTable(
                "dbo.Production",
                c => new
                    {
                        ProductionID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        ShiftID = c.Int(nullable: false),
                        PlantID = c.Int(nullable: false),
                        TargetMix = c.Int(nullable: false),
                        ActualMix = c.Int(nullable: false),
                        CrumbWaste = c.Int(nullable: false),
                        Cmp_Waste = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ProductionID)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .ForeignKey("dbo.Shift", t => t.ShiftID, cascadeDelete: true)
                .ForeignKey("dbo.Plant", t => t.PlantID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.ShiftID)
                .Index(t => t.PlantID);
            
            CreateTable(
                "dbo.Shift",
                c => new
                    {
                        ShiftID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ShiftID);
            
            CreateTable(
                "dbo.Plant",
                c => new
                    {
                        PlantID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        MixRatePerHour = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PlantID);
            
            CreateTable(
                "dbo.Downtime",
                c => new
                    {
                        DowntimeID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        ShiftID = c.Int(nullable: false),
                        PlantID = c.Int(nullable: false),
                        DowntimeTypeID = c.Int(nullable: false),
                        DowntimeType = c.String(),
                        Reason = c.String(),
                        Action = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.DowntimeID)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .ForeignKey("dbo.Shift", t => t.ShiftID, cascadeDelete: true)
                .ForeignKey("dbo.Plant", t => t.PlantID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.ShiftID)
                .Index(t => t.PlantID);
            
            CreateTable(
                "dbo.DowntimeType",
                c => new
                    {
                        DowntimeTypeID = c.Int(nullable: false, identity: true),
                        PlantID = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.DowntimeTypeID)
                .ForeignKey("dbo.Plant", t => t.PlantID, cascadeDelete: true)
                .Index(t => t.PlantID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.DowntimeType", new[] { "PlantID" });
            DropIndex("dbo.Downtime", new[] { "PlantID" });
            DropIndex("dbo.Downtime", new[] { "ShiftID" });
            DropIndex("dbo.Downtime", new[] { "UserID" });
            DropIndex("dbo.Production", new[] { "PlantID" });
            DropIndex("dbo.Production", new[] { "ShiftID" });
            DropIndex("dbo.Production", new[] { "UserID" });
            DropForeignKey("dbo.DowntimeType", "PlantID", "dbo.Plant");
            DropForeignKey("dbo.Downtime", "PlantID", "dbo.Plant");
            DropForeignKey("dbo.Downtime", "ShiftID", "dbo.Shift");
            DropForeignKey("dbo.Downtime", "UserID", "dbo.User");
            DropForeignKey("dbo.Production", "PlantID", "dbo.Plant");
            DropForeignKey("dbo.Production", "ShiftID", "dbo.Shift");
            DropForeignKey("dbo.Production", "UserID", "dbo.User");
            DropTable("dbo.DowntimeType");
            DropTable("dbo.Downtime");
            DropTable("dbo.Plant");
            DropTable("dbo.Shift");
            DropTable("dbo.Production");
            DropTable("dbo.User");
        }
    }
}
