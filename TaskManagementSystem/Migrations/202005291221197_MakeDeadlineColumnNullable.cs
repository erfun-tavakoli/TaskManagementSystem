namespace TaskManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeDeadlineColumnNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Projects", "Deadline", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Projects", "Deadline", c => c.DateTime(nullable: false));
        }
    }
}
