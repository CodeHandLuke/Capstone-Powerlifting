namespace PowerliftingCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedSavedWorkout : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SavedWorkouts", "Notes", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SavedWorkouts", "Notes");
        }
    }
}
