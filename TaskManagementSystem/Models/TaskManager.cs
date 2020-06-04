using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagementSystem.Models
{
    public class TaskManager
    {
        public bool CheckIfTaskPassedDeadline(Job job)
        {
            if (job.Deadline == null)
                return false;

            DateTime date = new DateTime();
            return (DateTime.Compare((DateTime)job.Deadline, date)) >= 0;
        }
        
    }
}