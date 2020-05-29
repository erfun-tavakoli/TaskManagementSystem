namespace TaskManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPriorityToProjectsAndJobs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "Priority", c => c.Int());
            AddColumn("dbo.Jobs", "Priority", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Jobs", "Priority");
            DropColumn("dbo.Projects", "Priority");
        }
    }
}
