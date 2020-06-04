using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagementSystem.Models
{
    public static class NotificationHelper
    {
        public static void CreateJobCompletedNotification(Job job)
        {
            Notification notification = new Notification();
            notification.ApplicationUser = job.Project.ApplicationUser;
            notification.Description = "The Task " + job.Title + " has been Completed";
        }

        public static void CreateProjectCompletedNotification(Project project)
        {
            Notification notification = new Notification();
            notification.ApplicationUser = project.ApplicationUser;
            notification.Description = "The Task " + project.Title + " has been Completed";
        }
    }
}