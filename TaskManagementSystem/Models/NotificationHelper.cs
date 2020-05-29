using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagementSystem.Models
{
    public class NotificationHelper
    {
        static ApplicationDbContext db = new ApplicationDbContext();

        public List<string> NotificationforDevelopers()
        {
            //var date = db.Tasks.Where(t => (t.DueDate.Date - DateTime.Now).Days == 1).Select(a => a.Notifications.Select(a=>a.Notifications.Select(t=>t.Message).ToList()));
            //var ss = db.Users.Select(q => q.Tasks.Where(t => (t.DueDate.Date - DateTime.Now).Days == 1));
            //var result = ss.Select(c=>c.)

            foreach(var user in db.Users)
            {
                var task = user.Tasks.Where(t => (t.DueDate.Date - DateTime.Now).Days == 1).ToList();
                foreach(var t in task)
                {
                    var result = t.Notifications.Select(s => s.Message).ToList();
                    return result;
                }
            }

            return null;
        }
    }
}