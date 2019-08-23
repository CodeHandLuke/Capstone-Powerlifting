namespace PowerliftingCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedLiftModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lifts", "Notes", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Lifts", "Notes");
        }
    }
}
