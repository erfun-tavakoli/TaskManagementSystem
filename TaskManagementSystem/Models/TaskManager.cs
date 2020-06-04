using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagementSystem.Models
{
    public class TaskManager
    {
        static ApplicationDbContext db = new ApplicationDbContext();

        public static void RemoveTask(int Id)
        {
            var task = db.Tasks.Find(Id);
            if (task != null)
            {
                db.Tasks.Remove(task);
            }
        }
    }
}