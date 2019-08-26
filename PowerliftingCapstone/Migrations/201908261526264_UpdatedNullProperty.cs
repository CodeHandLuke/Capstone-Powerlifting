namespace PowerliftingCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedNullProperty : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SavedWorkouts", "OneRMPercentage", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SavedWorkouts", "OneRMPercentage", c => c.Int(nullable: false));
        }
    }
}
