namespace PowerliftingCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WeightChange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserProfiles", "Weight", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserProfiles", "Weight", c => c.Int(nullable: false));
        }
    }
}
