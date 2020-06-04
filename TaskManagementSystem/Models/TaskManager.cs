using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagementSystem.Models
{
    public static class TaskManager
    {
        public static bool CheckIfTaskPassedDeadline(Job job)
        {
            if (job.Deadline == null)
                return false;

            DateTime date = DateTime.Now;
            return (DateTime.Compare((DateTime)job.Deadline, date)) >= 0;
        }
        
    }
}