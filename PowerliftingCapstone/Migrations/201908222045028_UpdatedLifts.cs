namespace PowerliftingCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedLifts : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Lifts", "OneRMPercentage", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Lifts", "OneRMPercentage", c => c.Int(nullable: false));
        }
    }
}
