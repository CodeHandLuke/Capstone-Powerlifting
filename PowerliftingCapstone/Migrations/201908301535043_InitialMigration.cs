namespace PowerliftingCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
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
                        SavedWorkoutDateId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ActualTotalId)
                .ForeignKey("dbo.UserProfiles", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserProfiles",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Age = c.Int(nullable: false),
                        Sex = c.String(),
                        Weight = c.Double(nullable: false),
                        Wilks = c.Double(nullable: false),
                        WorkoutOfDay = c.Int(),
                        ApplicationId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationId)
                .Index(t => t.ApplicationId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.ExpectedProgramTotals",
                c => new
                    {
                        ExpectedTotalId = c.Int(nullable: false, identity: true),
                        Exercise = c.String(),
                        Reps = c.Int(),
                        Weight = c.Double(),
                        SavedWorkoutDateId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ExpectedTotalId)
                .ForeignKey("dbo.UserProfiles", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Lifts",
                c => new
                    {
                        ProgramId = c.Int(nullable: false, identity: true),
                        SetOrder = c.Int(nullable: false),
                        WorkoutId = c.Int(nullable: false),
                        Exercise = c.String(),
                        OneRMPercentage = c.Int(),
                        Reps = c.Int(),
                        Weight = c.Double(),
                        Completed = c.Boolean(nullable: false),
                        Notes = c.Boolean(nullable: false),
                        NoteText = c.String(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProgramId)
                .ForeignKey("dbo.UserProfiles", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
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
                "dbo.Posts",
                c => new
                    {
                        PostId = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false),
                        PostText = c.String(),
                        ThreadId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PostId)
                .ForeignKey("dbo.Threads", t => t.ThreadId, cascadeDelete: true)
                .ForeignKey("dbo.UserProfiles", t => t.UserId, cascadeDelete: true)
                .Index(t => t.ThreadId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Threads",
                c => new
                    {
                        ThreadId = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false),
                        PostedBy = c.String(),
                        ThreadTitle = c.String(),
                        Posts = c.Int(nullable: false),
                        LastPost = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ThreadId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.SavedWorkoutDateTimes",
                c => new
                    {
                        SavedWorkoutDateTimeId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        WorkoutId = c.Int(nullable: false),
                        WorkoutNotes = c.String(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SavedWorkoutDateTimeId)
                .ForeignKey("dbo.UserProfiles", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.SavedWorkouts",
                c => new
                    {
                        SavedWorkoutId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Exercise = c.String(),
                        OneRMPercentage = c.Int(),
                        Reps = c.Int(),
                        Weight = c.Double(),
                        WorkoutId = c.Int(nullable: false),
                        NoteText = c.String(),
                        SavedWorkoutDateId = c.Int(nullable: false),
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
            DropForeignKey("dbo.SavedWorkoutDateTimes", "UserId", "dbo.UserProfiles");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Posts", "UserId", "dbo.UserProfiles");
            DropForeignKey("dbo.Posts", "ThreadId", "dbo.Threads");
            DropForeignKey("dbo.OneRepMaxes", "UserId", "dbo.UserProfiles");
            DropForeignKey("dbo.Lifts", "UserId", "dbo.UserProfiles");
            DropForeignKey("dbo.ExpectedProgramTotals", "UserId", "dbo.UserProfiles");
            DropForeignKey("dbo.ActualProgramTotals", "UserId", "dbo.UserProfiles");
            DropForeignKey("dbo.UserProfiles", "ApplicationId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.WeeklyTotals", new[] { "UserId" });
            DropIndex("dbo.SavedWorkouts", new[] { "UserId" });
            DropIndex("dbo.SavedWorkoutDateTimes", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Posts", new[] { "UserId" });
            DropIndex("dbo.Posts", new[] { "ThreadId" });
            DropIndex("dbo.OneRepMaxes", new[] { "UserId" });
            DropIndex("dbo.Lifts", new[] { "UserId" });
            DropIndex("dbo.ExpectedProgramTotals", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.UserProfiles", new[] { "ApplicationId" });
            DropIndex("dbo.ActualProgramTotals", new[] { "UserId" });
            DropTable("dbo.WorkoutSerializations");
            DropTable("dbo.WeeklyTotals");
            DropTable("dbo.SavedWorkouts");
            DropTable("dbo.SavedWorkoutDateTimes");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Threads");
            DropTable("dbo.Posts");
            DropTable("dbo.OneRepMaxes");
            DropTable("dbo.Lifts");
            DropTable("dbo.ExpectedProgramTotals");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.UserProfiles");
            DropTable("dbo.ActualProgramTotals");
        }
    }
}
