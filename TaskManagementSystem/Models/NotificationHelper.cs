using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagementSystem.Models
{
    public static class NotificationHelper
    {
        internal static ApplicationDbContext db = new ApplicationDbContext();
        public static void CreateJobCompletedNotificationForManager(Job job)
        {
            Notification notification = new Notification();
            notification.ApplicationUser = job.Project.ApplicationUser;
            notification.Description = "The Task " + job.Title + " has been Completed";
            db.Notifications.Add(notification);
            db.SaveChanges();
        }

        public static void CreateProjectCompletedNotification(Project project)
        {
            Notification notification = new Notification();
            notification.ApplicationUser = project.ApplicationUser;
            notification.Description = "The Task " + project.Title + " has been Completed";
        }
    }
}