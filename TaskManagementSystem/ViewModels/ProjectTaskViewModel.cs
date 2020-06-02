using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.ViewModels
{
    public class ProjectTaskViewModel
    {
        public Project Project { get; set; }
        public IEnumerable<Job> Tasks { get; set; }
        
    }
}