using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TaskManagementSystem.Models
{
    public class Job
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }
        public DateTime DateCreated { get; set; }
        public Boolean IsComplete { get; set; }
        public int CompletionPercentage { get; set; }
        public string Note { get; set; }
        public DateTime? Deadline { get; set; }
        public DateTime? DateCompleted { get; set; }
        public Priority? Priority { get; set; }
        [ForeignKey("ApplicationUser")]
        public virtual ICollection<ApplicationUser> Developers { get; set; }
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }

        public Job()
        {
            DateCreated = DateTime.Now;
            Developers = new HashSet<ApplicationUser>();
        }
    }

    public enum Priority
    {
        Low,
        Average,
        High
    }
}