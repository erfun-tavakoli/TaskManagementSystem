using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagementSystem.Models
{
    public class Notification
    {
        public int Id { get; set; }
        
        public int  JobId { get; set; }
        public virtual Job Job { get; set; }

        public int  ProjectId { get; set; }
        public virtual Project Project { get; set; }
        public string Message { get; set; }

        


    }
}