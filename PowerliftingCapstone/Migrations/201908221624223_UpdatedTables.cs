namespace PowerliftingCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActualProgramTotals",
                c => new
                    {
                        ActualTotalId = c.Int(nullable: false, identity: true),
                        Exercise = c.String(),
                        Reps = c.Int(),
                        Weight = c.Double(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ActualTotalId)
                .ForeignKey("dbo.UserProfiles", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserProfiles",
                c => new
                    {
                        UserProfileId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Age = c.Int(nullable: false),
                        Sex = c.String(),
                        Weight = c.Int(nullable: false),
                        Wilks = c.Double(nullable: false),
                        WorkoutOfDay = c.Int(),
                        ApplicationId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserProfileId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationId)
                .Index(t => t.ApplicationId);
            
            CreateTable(
                "dbo.ExpectedProgramTotals",
                c => new
                    {
                        ExpectedTotalId = c.Int(nullable: false, identity: true),
                        Exercise = c.String(),
                        Reps = c.Int(nullable: false),
                        Weight = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ExpectedTotalId);
            
            CreateTable(
                "dbo.Lifts",
                c => new
                    {
                        ProgramId = c.Int(nullable: false, identity: true),
                        SetOrder = c.Int(nullable: false),
                        WorkoutId = c.Int(nullable: false),
                        Exercise = c.String(),
                        OneRMPercentage = c.Int(nullable: false),
                        Reps = c.Int(),
                        Weight = c.Double(),
                        Completed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ProgramId);
            
            CreateTable(
                "dbo.OneRepMaxes",
                c => new
                    {
                        OneRepMaxId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Squat = c.Double(nullable: false),
                        Bench = c.Double(nullable: false),
                        Deadlift = c.Double(nullable: false),
                        Total = c.Double(nullable: false),
                        Wilks = c.Double(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OneRepMaxId)
                .ForeignKey("dbo.UserProfiles", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.SavedWorkoutDateTimes",
                c => new
                    {
                        SavedWorkoutDateId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        WorkoutId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SavedWorkoutDateId);
            
            CreateTable(
                "dbo.SavedWorkouts",
                c => new
                    {
                        SavedWorkoutId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Exercise = c.String(),
                        OneRMPercentage = c.Int(nullable: false),
                        Reps = c.Int(),
                        Weight = c.Double(),
                        WorkoutId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SavedWorkoutId)
                .ForeignKey("dbo.UserProfiles", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.WeeklyTotals",
                c => new
                    {
                        WeeklyTotalId = c.Int(nullable: false, identity: true),
                        Week = c.Int(nullable: false),
                        Exercise = c.String(),
                        Reps = c.Int(),
                        Weight = c.Double(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.WeeklyTotalId)
                .ForeignKey("dbo.UserProfiles", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.WorkoutSerializations",
                c => new
                    {
                        WorkoutSerialId = c.Int(nullable: false, identity: true),
                        WorkoutId = c.Int(nullable: false),
                        WeekDay = c.String(),
                    })
                .PrimaryKey(t => t.WorkoutSerialId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WeeklyTotals", "UserId", "dbo.UserProfiles");
            DropForeignKey("dbo.SavedWorkouts", "UserId", "dbo.UserProfiles");
            DropForeignKey("dbo.OneRepMaxes", "UserId", "dbo.UserProfiles");
            DropForeignKey("dbo.ActualProgramTotals", "UserId", "dbo.UserProfiles");
            DropForeignKey("dbo.UserProfiles", "ApplicationId", "dbo.AspNetUsers");
            DropIndex("dbo.WeeklyTotals", new[] { "UserId" });
            DropIndex("dbo.SavedWorkouts", new[] { "UserId" });
            DropIndex("dbo.OneRepMaxes", new[] { "UserId" });
            DropIndex("dbo.UserProfiles", new[] { "ApplicationId" });
            DropIndex("dbo.ActualProgramTotals", new[] { "UserId" });
            DropTable("dbo.WorkoutSerializations");
            DropTable("dbo.WeeklyTotals");
            DropTable("dbo.SavedWorkouts");
            DropTable("dbo.SavedWorkoutDateTimes");
            DropTable("dbo.OneRepMaxes");
            DropTable("dbo.Lifts");
            DropTable("dbo.ExpectedProgramTotals");
            DropTable("dbo.UserProfiles");
            DropTable("dbo.ActualProgramTotals");
        }
    }
}
