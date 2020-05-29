namespace TaskManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class allowMultipleDevelopersForJobs : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Jobs", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Jobs", new[] { "ApplicationUserId" });
            CreateTable(
                "dbo.JobApplicationUsers",
                c => new
                    {
                        Job_Id = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Job_Id, t.ApplicationUser_Id })
                .ForeignKey("dbo.Jobs", t => t.Job_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .Index(t => t.Job_Id)
                .Index(t => t.ApplicationUser_Id);
            
            DropColumn("dbo.Jobs", "ApplicationUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Jobs", "ApplicationUserId", c => c.String(maxLength: 128));
            DropForeignKey("dbo.JobApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.JobApplicationUsers", "Job_Id", "dbo.Jobs");
            DropIndex("dbo.JobApplicationUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.JobApplicationUsers", new[] { "Job_Id" });
            DropTable("dbo.JobApplicationUsers");
            CreateIndex("dbo.Jobs", "ApplicationUserId");
            AddForeignKey("dbo.Jobs", "ApplicationUserId", "dbo.AspNetUsers", "Id");
        }
    }
}
