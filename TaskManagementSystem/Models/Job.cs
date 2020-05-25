using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        public double DailyWage { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime DateCompleted { get; set; }
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public Job()
        {
            DateCreated = DateTime.Now;
        }
    }
}