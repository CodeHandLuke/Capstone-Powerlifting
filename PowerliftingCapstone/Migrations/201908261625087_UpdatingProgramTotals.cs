namespace PowerliftingCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatingProgramTotals : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExpectedProgramTotals", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.ExpectedProgramTotals", "Reps", c => c.Int());
            AlterColumn("dbo.ExpectedProgramTotals", "Weight", c => c.Double());
            CreateIndex("dbo.ExpectedProgramTotals", "UserId");
            AddForeignKey("dbo.ExpectedProgramTotals", "UserId", "dbo.UserProfiles", "UserProfileId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExpectedProgramTotals", "UserId", "dbo.UserProfiles");
            DropIndex("dbo.ExpectedProgramTotals", new[] { "UserId" });
            AlterColumn("dbo.ExpectedProgramTotals", "Weight", c => c.Double(nullable: false));
            AlterColumn("dbo.ExpectedProgramTotals", "Reps", c => c.Int(nullable: false));
            DropColumn("dbo.ExpectedProgramTotals", "UserId");
        }
    }
}
