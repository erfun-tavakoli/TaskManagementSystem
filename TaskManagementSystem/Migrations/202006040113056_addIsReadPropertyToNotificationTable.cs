namespace TaskManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addIsReadPropertyToNotificationTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "IsRead", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notifications", "IsRead");
        }
    }
}
