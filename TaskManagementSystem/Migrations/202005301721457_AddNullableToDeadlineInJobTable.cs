namespace TaskManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNullableToDeadlineInJobTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Jobs", "Deadline", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Jobs", "Deadline", c => c.DateTime(nullable: false));
        }
    }
}
