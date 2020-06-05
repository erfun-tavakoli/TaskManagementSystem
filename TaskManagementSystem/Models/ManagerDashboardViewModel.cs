using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagementSystem.Models
{
    public class ManagerDashboardViewModel
    {
        public List<Project> Projects { get; set; }
        public int NumOfUnreadNotifications { get; set; }
        public string UserId { get; set; }
    }
}