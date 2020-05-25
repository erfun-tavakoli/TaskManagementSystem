using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagementSystem.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double Budget { get; set; }
        public DateTime DateCreated { get; set; }
        public virtual ICollection<Job> Tasks { get; set; }
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public Project()
        {
            this.Tasks = new HashSet<Job>();
            DateCreated = DateTime.Now;
        }
    }
}