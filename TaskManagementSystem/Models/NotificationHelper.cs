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
            db.Notifications.Add(notification);
            db.SaveChanges();
        }

        public static void CreateBugReportedNotification(Job job, string developerName)
        {
            Notification notification = new Notification();
            notification.ApplicationUserId = job.Project.ApplicationUserId;
            notification.Description = "A bug has been reported on task " + job.Title + " By " + developerName + ".";
            db.Notifications.Add(notification);
            db.SaveChanges();
        }

        public static int NumOfUnopenedNotifications(ApplicationUser user)
        {
            return user.Notifications.Count(n => !n.IsRead);
        }
    }
}