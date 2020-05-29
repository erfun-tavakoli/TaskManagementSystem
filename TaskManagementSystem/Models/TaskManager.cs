using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagementSystem.Models
{
    public class TaskManager
    {


        static ApplicationDbContext db = new ApplicationDbContext();


        public int NumberOfNotification()
        {
            var task = db.Tasks.Where(t => (t.DueDate.Date - DateTime.Now).Days == 1);
            foreach(var t in task)
            {
                t.NotificationCount = t.NotificationCount + 1;

            }

        }







    }
}